using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Auth.Auth.Api.Utilities.Validators;

namespace Auth.Auth.Api.Controllers.Application.Requests
{
    public class ApplicationCreateRequest
    {
        [Required] public string CreatorId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required, Url] public string WebsiteUrl { get; set; }
        [Required, Url] public string RedirectUrl { get; set; }
        [Required, PopulatedList] public List<string> Scopes { get; set; }
    }
}