using System.Threading.Tasks;
using Auth.Api.Controllers.Auth.Queries;
using Auth.Api.Services.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.Auth
{
    [ApiController]
    [Route("Token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Exchange([FromQuery] TokenAuthQuery tokenAuthQuery)
        {
            return Ok();
        }
    }
}