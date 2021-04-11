using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Api.Services.TokenService.Exceptions;
using Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;
using Auth.Core.Services.TimeService;

namespace Auth.Api.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly TokenFactory _tokenFactory;
        private readonly ITimeService _timeService;
        private readonly Dictionary<string, CodeToken> _codes = new Dictionary<string, CodeToken>();
        private readonly Dictionary<string, AccessToken> _tokens = new Dictionary<string, AccessToken>();

        public TokenService(TokenFactory tokenFactory, ITimeService timeService)
        {
            _tokenFactory = tokenFactory;
            _timeService = timeService;
        }
        
        public async Task<AccessToken> GetAccessToken(string grantType, Application application)
        {
            EnsureCorrectGrantTypeAndApplicationPairs(grantType, application.FirstParty);
            
            var token = _tokenFactory.Create();

            _tokens.Add(token.Token, token);

            return token;
        }

        public async Task<bool> HasCode(string clientId, string code)
        {
            bool exists = _codes.TryGetValue(code, out var codeToken);
            if (!exists) return false;

            var currentDateTime = _timeService.GetDateTime();
            if (currentDateTime >= codeToken.ExpiresAt)
            {
                exists = false;
                _codes.Remove(code);
            }

            return exists;
        }

        public async Task<AccessToken> Get(string token)
        {
            bool exists = _tokens.TryGetValue(token, out var model);
            if (!exists) return null;

            if (model.ExpiresAt <= _timeService.GetDateTime())
            {
                exists = false;
                _tokens.Remove(token);
            }

            return exists ? model : null;
        }

        private void EnsureCorrectGrantTypeAndApplicationPairs(string grantType, bool firstPartyApplication)
        {
            if (firstPartyApplication && grantType != TokenServiceConstants.PasswordGrantType)
            {
                throw new FirstPartyApplicationMustUsePasswordGrantTypeException();
            }
            if (!firstPartyApplication && grantType == TokenServiceConstants.PasswordGrantType)
            {
                throw new PasswordGrantTypeNotAllowedException();
            }
        }
    }
}