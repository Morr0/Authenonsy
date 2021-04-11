using System.Threading.Tasks;
using Auth.Api.Controllers.Auth.Requests;
using Auth.Api.Controllers.Auth.Responses;
using Auth.Api.Services.ApplicationService;
using Auth.Api.Services.TokenService;
using Auth.Api.Services.TokenService.Exceptions;
using Auth.Api.Services.UserService;
using HybridModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.Auth
{
    [ApiController]
    [Route("Token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;

        public TokenController(ITokenService tokenService, IApplicationService applicationService, IUserService userService)
        {
            _tokenService = tokenService;
            _applicationService = applicationService;
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Exchange([FromHybrid] ExchangeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            // Use ExchangeRequest
            if (request.GrantType == TokenServiceConstants.PasswordGrantType)
            {
                bool existingUser = await _userService.Exists(request.Username, request.Password).ConfigureAwait(false);
                if (!existingUser) return NotFound(new ExchangeErrorResponse
                {
                    Error = $"Username {request.Username} does not exist"
                });
            } 
            else if (request.GrantType == TokenServiceConstants.CodeGrantType)
            {
                bool hasCode = await _tokenService.HasCode(request.ClientId, request.Code).ConfigureAwait(false);
                if (!hasCode) return NotFound(new ExchangeErrorResponse
                {
                    Error = $"Code {request.Code} does not exist or has expired"
                });
            }
            
            var application = await _applicationService.Get(request.ClientId).ConfigureAwait(false);
            if (application is null) return NotFound(new ExchangeErrorResponse
            {
                Error = $"Application Client Id {request.ClientId} does not exist"
            });

            try
            {
                var accessToken = await _tokenService.GetAccessToken(request.GrantType, application)
                    .ConfigureAwait(false);

                return Ok(accessToken);
            }
            catch (FirstPartyApplicationMustUsePasswordGrantTypeException)
            {
                return BadRequest(new ExchangeErrorResponse
                {
                    Error = $"Application Client Id {request.ClientId} must use 'grant_type={TokenServiceConstants.PasswordGrantType}'"
                });
            }
            catch (PasswordGrantTypeNotAllowedException)
            {
                return BadRequest(new ExchangeErrorResponse
                {
                    Error = $"Application Client Id {request.ClientId} must use 'grant_type={TokenServiceConstants.CodeGrantType}'"
                });
            }
        }

        [HttpPost("Valid")]
        public async Task<IActionResult> ValidToken([FromBody] ValidTokenRequest request)
        {
            var accessToken = await _tokenService.Get(request.AccessToken).ConfigureAwait(false);
            if (accessToken is null) return Unauthorized();

            return Ok(accessToken);
        }
    }
}