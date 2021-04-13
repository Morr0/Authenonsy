using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Auth.Api.Services.TokenService.Exceptions;
using Auth.Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;
using Auth.Core.Services.TimeService;

namespace Auth.Auth.Api.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly TokenFactory _tokenFactory;
        private readonly ITimeService _timeService;
        private readonly Dictionary<string, CodeToken> _codes = new Dictionary<string, CodeToken>();
        private readonly Dictionary<string, AccessToken> _accessTokens = new Dictionary<string, AccessToken>();
        private readonly Dictionary<string, AccessToken> _refreshTokens = new Dictionary<string, AccessToken>();

        public TokenService(TokenFactory tokenFactory, ITimeService timeService)
        {
            _tokenFactory = tokenFactory;
            _timeService = timeService;
        }
        
        public async Task<AccessToken> GetAccessToken(string grantType, Application application)
        {
            EnsureCorrectGrantTypeAndApplicationPairs(grantType, application.FirstParty);
            
            var token = _tokenFactory.Create();

            _accessTokens.Add(token.Token, token);
            _refreshTokens.Add(token.RefreshToken, token);

            return token;
        }

        public async Task<AccessToken> Refresh(string refreshToken)
        {
            bool exists = _refreshTokens.TryGetValue(refreshToken, out var oldToken);
            if (!exists) return null;

            if (_accessTokens.ContainsKey(oldToken.Token)) _accessTokens.Remove(oldToken.Token);
            _refreshTokens.Remove(refreshToken);
            
            var token = _tokenFactory.Create();

            _accessTokens.Add(token.Token, token);
            _refreshTokens.Add(token.RefreshToken, token);

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
            bool exists = _accessTokens.TryGetValue(token, out var model);
            if (!exists) return null;

            if (model.ExpiresAt <= _timeService.GetDateTime())
            {
                exists = false;
                _accessTokens.Remove(token);
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