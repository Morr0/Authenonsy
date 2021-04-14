using System.ComponentModel.DataAnnotations;

namespace Auth.Auth.Api.Controllers.Auth.Requests
{
    public class ExchangeCodeToAccessTokenRequest
    {
        [Required] public string ClientId { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string Code { get; set; }
    }
}