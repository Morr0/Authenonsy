namespace Auth.Auth.Api.Controllers.User.Requests
{
    public class UserCreateRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}