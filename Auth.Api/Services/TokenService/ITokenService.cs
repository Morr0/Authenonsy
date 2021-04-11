using System.Threading.Tasks;
using Auth.Api.Services.TokenService.Models;
using Auth.Core.Models;

namespace Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        Task<AccessToken> GetAccessToken(string grantType, Application application);
        Task<bool> HasCode(string clientId, string code);
    }
}