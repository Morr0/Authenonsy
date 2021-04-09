using System.Threading.Tasks;
using Auth.Api.Controllers.User.Requests;
using Auth.Api.Controllers.User.Response;
using Auth.Api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.User
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest userCreateRequestDto)
        {
            var user = await _userService.Create(userCreateRequestDto).ConfigureAwait(false);

            return Ok(new UserCreateResponse
            {
                UserId = user.Id,
                Username = user.Username
            });
        }

        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var user = await _userService.GetById(userId).ConfigureAwait(false);
            if (user is null) return NotFound();

            return Ok(new GetUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Username = user.Username,
                Since = user.CreatedAt.ToString("s")
            });
        }
        
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            var user = await _userService.GetByUsername(username).ConfigureAwait(false);
            if (user is null) return NotFound();

            return Ok(new GetUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Username = user.Username,
                Since = user.CreatedAt.ToString("s")
            });
        }
    }
}