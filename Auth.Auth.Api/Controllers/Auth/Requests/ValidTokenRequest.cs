using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Auth.Api.Controllers.Auth.Requests
{
    public class ValidTokenRequest
    {
        [Required, MinLength(1), JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}