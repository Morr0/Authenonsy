using System.Threading.Tasks;
using Auth.Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;

namespace Auth.Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        Task<AccessToken> GetAccessToken(string grantType, Application application);
        Task<AccessToken> Refresh(string refreshToken);
        Task<bool> HasCode(string clientId, string code);
        Task<AccessToken> Get(string token);
    }
}