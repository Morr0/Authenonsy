using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.Auth.Requests;
using Auth.Auth.Api.Controllers.Auth.Responses;
using Auth.Auth.Api.Services.ApplicationService;
using Auth.Auth.Api.Services.TokenService;
using Auth.Auth.Api.Services.TokenService.Exceptions;
using Auth.Auth.Api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Auth.Api.Controllers.Auth
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

        [HttpPost("Password")]
        public async Task<IActionResult> ExchangePassword([FromBody] ExchangePasswordToAccessTokenRequest request)
        {
            var user = await _userService.GetByUsername(request.Username).ConfigureAwait(false);
            if (user is null || !_userService.IsCorrectPassword(user, request.Password)) return NotFound();

            var application = await _applicationService.Get(request.ClientId).ConfigureAwait(false);
            if (application is null) return NotFound();

            try
            {
                var accessToken = await _tokenService.ExchangePasswordForToken(application, user).ConfigureAwait(false);
                return Ok(new AccessTokenResponse
                {
                    AccessToken = accessToken.Token,
                    RefreshToken = accessToken.RefreshToken,
                    AccessTokenExpiresAt = accessToken.ExpiresAt.ToString("s")
                });
            }
            catch (PasswordGrantTypeNotAllowedException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("Code")]
        public async Task<IActionResult> GetCode([FromBody] GetCodeRequest request)
        {
            var application = await _applicationService.Get(request.ClientId).ConfigureAwait(false);
            if (application is null) return NotFound();

            try
            {
                var code = await _tokenService.GetCode(application, request.AccessToken).ConfigureAwait(false);
                if (code is null) return StatusCode(403);

                return Ok(new CodeResponse
                {
                    Code = code.Code,
                    ExpiresAt = code.ExpiresAt.ToString("s")
                });
            }
            catch (FirstPartyApplicationMustUsePasswordGrantTypeException)
            {
                return BadRequest();
            }
            catch (ExpiredAccessTokenException)
            {
                return NotFound(new
                {
                    Error = $"Access Token: {request.AccessToken} has expired"
                });
            }
        }

        [HttpPost("Token")]
        public async Task<IActionResult> ExchangeCodeForAccessToken([FromBody] ExchangeCodeToAccessTokenRequest request)
        {
            var application = await _applicationService.Get(request.ClientId).ConfigureAwait(false);
            if (application is null) return NotFound();
            if (application.ClientSecret != request.ClientSecret) return StatusCode(403);

            try
            {
                var accessToken = await _tokenService.ExchangeCodeForToken(application, request.Code).ConfigureAwait(false);
                if (accessToken is null) return StatusCode(403);
                
                return Ok(new AccessTokenResponse
                {
                    AccessToken = accessToken.Token,
                    RefreshToken = accessToken.RefreshToken,
                    AccessTokenExpiresAt = accessToken.ExpiresAt.ToString("s")
                });
            }
            catch (FirstPartyApplicationMustUsePasswordGrantTypeException)
            {
                return BadRequest();
            }
            catch (ExpiredCodeException)
            {
                return NotFound(new
                {
                    Error = $"Code: {request.Code} has expired"
                });
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshAccessTokenRequest request)
        {
            var newAccessToken = await _tokenService.RefreshAccessToken(request.AccessToken, request.RefreshToken).ConfigureAwait(false);
            if (newAccessToken is null) return Forbid();

            return Ok(new AccessTokenResponse
            {
                AccessToken = newAccessToken.Token,
                AccessTokenExpiresAt = newAccessToken.ExpiresAt.ToString("s"),
                RefreshToken = newAccessToken.RefreshToken
            });
        }
        
        [HttpPost("Valid")]
        public async Task<IActionResult> ValidToken([FromBody] ValidTokenRequest request)
        {
            try
            {
                var accessToken = await _tokenService.GetAccessToken(request.AccessToken).ConfigureAwait(false);
                if (accessToken is null) return Unauthorized();

                return Ok();
            }
            catch (ExpiredAccessTokenException)
            {
                return NotFound(new
                {
                    Error = $"Access Token: {request.AccessToken} has expired"
                });
            }
        }
    }
}