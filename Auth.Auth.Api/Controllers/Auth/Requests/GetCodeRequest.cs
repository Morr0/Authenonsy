using System.ComponentModel.DataAnnotations;

namespace Auth.Auth.Api.Controllers.Auth.Requests
{
    public class GetCodeRequest
    {
        [Required] public string ClientId { get; set; }
        [Required] public string AccessToken { get; set; }
    }
}