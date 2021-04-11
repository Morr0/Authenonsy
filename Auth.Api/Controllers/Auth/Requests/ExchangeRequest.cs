using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Auth.Api.Services.TokenService;
using HybridModelBinding;

namespace Auth.Api.Controllers.Auth.Requests
{
    [HybridBindClass(new []{Source.Body, Source.QueryString})]
    public class ExchangeRequest : IValidatableObject
    {
        [HybridBindProperty(Source.QueryString, "grant_type"), Required] public string GrantType { get; set; }
        [HybridBindProperty(Source.QueryString, "client_id"), Required] public string ClientId { get; set; }
        
        [HybridBindProperty(Source.Body)] public string Code { get; set; }
        [HybridBindProperty(Source.Body)] public string Username { get; set; }
        [HybridBindProperty(Source.Body)] public string Password { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = new LinkedList<ValidationResult>();
            
            if (GrantType == TokenServiceConstants.PasswordGrantType &&
                (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)))
            {
                list.AddLast(new ValidationResult("Please provide a username and password"));
            } 
            else if (GrantType == TokenServiceConstants.CodeGrantType && string.IsNullOrEmpty(Code))
            {
                list.AddLast(new ValidationResult("Please provide an authorization code"));
            }

            return list;
        }
    }
}