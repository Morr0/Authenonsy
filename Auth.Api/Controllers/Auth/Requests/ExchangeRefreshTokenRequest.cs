using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Api.Controllers.Auth.Requests
{
    public class ExchangeRefreshTokenRequest
    {
        [Required, MinLength(1), JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }
        [Required, MinLength(1), JsonPropertyName("client_id")] public string ClientId { get; set; }
        [Required, MinLength(1), JsonPropertyName("client_secret")] public string ClientSecret { get; set; }
    }
}