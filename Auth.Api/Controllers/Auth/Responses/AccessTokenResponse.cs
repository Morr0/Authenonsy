using System.Text.Json.Serialization;

namespace Auth.Api.Controllers.Auth.Responses
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; }
        [JsonPropertyName("access_token_expiry")] public string AccessTokenExpiresAt { get; set; }
        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }
    }
}