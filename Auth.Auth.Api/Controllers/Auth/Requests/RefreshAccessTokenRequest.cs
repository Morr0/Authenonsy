using System.ComponentModel.DataAnnotations;

namespace Auth.Auth.Api.Controllers.Auth.Requests
{
    public class RefreshAccessTokenRequest
    {
        [Required] public string AccessToken { get; set; }
        [Required] public string RefreshToken { get; set; }
    }
}