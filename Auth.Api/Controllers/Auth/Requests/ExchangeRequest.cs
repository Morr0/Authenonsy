namespace Auth.Api.Controllers.Auth.Requests
{
    public class ExchangeRequest
    {
        public string Code { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}