using System.Threading.Tasks;
using Auth.Auth.Api.Services.TokenService.Exceptions;
using Auth.Auth.Api.Services.TokenService.Models;
using Auth.Core.Factories;
using Auth.Core.Models;
using Auth.Core.Models.Auth;
using Auth.Core.Services.TimeService;
using Auth.Data.Repositories.Database;
using Microsoft.EntityFrameworkCore;

namespace Auth.Auth.Api.Services.TokenService
{
    // TODO check expiry values
    public class TokenService : ITokenService
    {
        private readonly DatabaseContext _context;
        private readonly UserApplicationFactory _userApplicationFactory;

        public TokenService(DatabaseContext context, UserApplicationFactory userApplicationFactory)
        {
            _context = context;
            _userApplicationFactory = userApplicationFactory;
        }

        public async Task<CodeToken> GetCode(Application application, string accessToken)
        {
            if (application.FirstParty) throw new FirstPartyApplicationMustUsePasswordGrantTypeException();
            
            // No need to check if has user access because if there is access token
            var actualAccessTokenSession = await _context.UserApplicationSession
                .Include(x => x.ApplicationAccess)
                .FirstOrDefaultAsync(x => x.AccessToken == accessToken)
                .ConfigureAwait(false);
            if (actualAccessTokenSession is null) return null;
            if (!actualAccessTokenSession.CanIssueCode) return null; 

            var userApplicationCodeRequest = _userApplicationFactory.CreateCode(actualAccessTokenSession.ApplicationAccess);
            
            await _context.UserApplicationCodeRequest.AddAsync(userApplicationCodeRequest).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new CodeToken
            {
                Code = userApplicationCodeRequest.Code,
                CreatedAt = userApplicationCodeRequest.CreatedAt,
                ExpiresAt = userApplicationCodeRequest.ExpiresAt
            };
        }

        public async Task<AccessToken> ExchangeCodeForToken(Application application, string code)
        {
            if (application.FirstParty) throw new FirstPartyApplicationMustUsePasswordGrantTypeException();

            var userApplicationCodeRequest = await _context.UserApplicationCodeRequest
                .Include(x => x.ApplicationAccess)
                .FirstOrDefaultAsync(x => x.Code == code).ConfigureAwait(false);
            if (userApplicationCodeRequest is null) return null;

            var userApplicationSession =
                _userApplicationFactory.CreateSession(userApplicationCodeRequest.ApplicationAccess);

            _context.Remove(userApplicationCodeRequest);
            await _context.UserApplicationSession.AddAsync(userApplicationSession).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new AccessToken
            {
                Token = userApplicationSession.AccessToken,
                CreatedAt = userApplicationSession.CreatedAt,
                ExpiresAt = userApplicationSession.ExpiresAt,
                RefreshToken = userApplicationSession.RefreshToken
            };
        }

        public async Task<AccessToken> ExchangePasswordForToken(Application application, User user)
        {
            if (!application.FirstParty) throw new PasswordGrantTypeNotAllowedException();
            
            var userApplicationAccess = await EnsureUserApplicationAccessCreated(application, user);
            var userApplicationSession = _userApplicationFactory.CreateSession(userApplicationAccess, true);

            await _context.UserApplicationSession.AddAsync(userApplicationSession).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new AccessToken
            {
                Token = userApplicationSession.AccessToken,
                CreatedAt = userApplicationSession.CreatedAt,
                ExpiresAt = userApplicationSession.ExpiresAt,
                RefreshToken = userApplicationSession.RefreshToken
            };
        }

        private async Task<UserApplicationAccess> EnsureUserApplicationAccessCreated(Application application, User user)
        {
            var userApplicationAccess = await _context.UserApplicationAccess
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.ApplicationClientId == application.ClientId)
                .ConfigureAwait(false);

            return userApplicationAccess ??
                   _userApplicationFactory.CreateAccess(user.Id, application.ClientId, application.Scopes);
        }

        public async Task<AccessToken> RefreshAccessToken(string oldAccessToken, string refreshToken)
        {
            var oldUserApplicationSession = await _context.UserApplicationSession
                .Include(x => x.ApplicationAccess)
                .FirstOrDefaultAsync(x => x.AccessToken == oldAccessToken)
                .ConfigureAwait(false);
            if (oldUserApplicationSession is null) return null;
            if (oldUserApplicationSession.RefreshToken != refreshToken) return null;

            var newUserApplicationSession =
                _userApplicationFactory.CreateSession(oldUserApplicationSession.ApplicationAccess);

            _context.Remove(oldUserApplicationSession);
            await _context.UserApplicationSession.AddAsync(newUserApplicationSession).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            
            return new AccessToken
            {
                Token = newUserApplicationSession.AccessToken,
                CreatedAt = newUserApplicationSession.CreatedAt,
                ExpiresAt = newUserApplicationSession.ExpiresAt,
                RefreshToken = newUserApplicationSession.RefreshToken
            };
        }

        public async Task<AccessToken> GetAccessToken(string token)
        {
            var userApplicationSession = await _context.UserApplicationSession.AsNoTracking()
                .Include(x => x.ApplicationAccess)
                .FirstOrDefaultAsync(x => x.AccessToken == token)
                .ConfigureAwait(false);
            if (userApplicationSession is null) return null;
            
            return new AccessToken
            {
                Token = userApplicationSession.AccessToken,
                CreatedAt = userApplicationSession.CreatedAt,
                ExpiresAt = userApplicationSession.ExpiresAt,
                RefreshToken = userApplicationSession.RefreshToken
            };
        }
    }
}