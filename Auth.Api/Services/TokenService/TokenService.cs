using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Api.Services.TokenService.Exceptions;
using Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;

namespace Auth.Api.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly TokenFactory _tokenFactory;
        private readonly Dictionary<string, AccessToken> _tokens = new Dictionary<string, AccessToken>();

        public TokenService(TokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }
        
        public async Task<AccessToken> GetAccessToken(string grantType, Application application)
        {
            EnsureCorrectGrantTypeAndApplicationPairs(grantType, application.FirstParty);
            
            var token = _tokenFactory.Create();

            _tokens.Add(token.Token, token);

            return token;
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