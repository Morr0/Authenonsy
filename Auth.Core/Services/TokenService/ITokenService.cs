using Auth.Core.Services.TokenService.Models;

namespace Auth.Core.Services.TokenService
{
    public interface ITokenService
    {
        AccessToken GetAccessToken(string userId);
    }
}