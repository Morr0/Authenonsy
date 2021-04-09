using Auth.Api.Services.TokenService.Models;

namespace Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        AccessToken GetAccessToken(string userId);
    }
}