namespace Auth.Auth.Api.Controllers.Application.Requests
{
    public class ApplicationCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string RedirectUrl { get; set; }
    }
}