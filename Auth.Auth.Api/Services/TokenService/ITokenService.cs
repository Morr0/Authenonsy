using System.Threading.Tasks;
using Auth.Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;

namespace Auth.Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        Task<CodeToken> GetCode(Application application, string accessToken);
        Task<AccessToken> ExchangeCodeForToken(Application application, string code);
        Task<AccessToken> ExchangePasswordForToken(Application application, User user);
        Task<AccessToken> GetAccessToken(string token);
        Task<AccessToken> RefreshAccessToken(string oldAccessToken, string refreshToken);
    }
}