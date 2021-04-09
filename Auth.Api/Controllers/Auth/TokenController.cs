using System.Threading.Tasks;
using Auth.Api.Controllers.Auth.Queries;
using Auth.Api.Controllers.Auth.Requests;
using Auth.Api.Controllers.Auth.Responses;
using Auth.Api.Services.ApplicationService;
using Auth.Api.Services.TokenService;
using Auth.Api.Services.TokenService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.Auth
{
    [ApiController]
    [Route("Token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IApplicationService _applicationService;

        public TokenController(ITokenService tokenService, IApplicationService applicationService)
        {
            _tokenService = tokenService;
            _applicationService = applicationService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Exchange([FromQuery] TokenAuthQuery tokenAuthQuery, [FromBody] ExchangeRequest exchangeRequest)
        {
            // Use ExchangeRequest
            var application = await _applicationService.Get(tokenAuthQuery.ClientId).ConfigureAwait(false);
            if (application is null) return NotFound(new ExchangeErrorResponse
            {
                Error = $"Application Client Id: {tokenAuthQuery.ClientId} does not exist"
            });

            try
            {
                var accessToken = await _tokenService.GetAccessToken(tokenAuthQuery.GrantType, application)
                    .ConfigureAwait(false);

                return Ok(accessToken);
            }
            catch (FirstPartyApplicationMustUsePasswordGrantTypeException)
            {
                return BadRequest(new ExchangeErrorResponse
                {
                    Error = $"Application Client Id: {tokenAuthQuery.ClientId} must use 'grant_type={TokenServiceConstants.PasswordGrantType}'"
                });
            }
            catch (PasswordGrantTypeNotAllowedException)
            {
                return BadRequest(new ExchangeErrorResponse
                {
                    Error = $"Application Client Id: {tokenAuthQuery.ClientId} must use 'grant_type={TokenServiceConstants.CodeGrantType}'"
                });
            }
        }
    }
}