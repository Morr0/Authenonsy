using System.Text.Json.Serialization;

namespace Auth.Auth.Api.Controllers.Auth.Responses
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public string AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}