using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Auth.Api.Controllers.Auth.Requests
{
    public class ExchangePasswordToAccessTokenRequest
    {
        [Required] public string ClientId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}