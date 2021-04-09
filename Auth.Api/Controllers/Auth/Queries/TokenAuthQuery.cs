using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Auth.Api.Services.TokenService;

namespace Auth.Api.Controllers.Auth.Queries
{
    public class TokenAuthQuery : IValidatableObject
    {
        [JsonPropertyName("grant_type"), Required] public string GrantType { get; set; }
        [JsonPropertyName("client_id"), Required] public string ClientId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new LinkedList<ValidationResult>();
            
            EnsureValidGrantType(errors);

            return errors;
        }

        private void EnsureValidGrantType(LinkedList<ValidationResult> errors)
        {
            bool valid = GrantType == TokenServiceConstants.CodeGrantType ||
                         GrantType == TokenServiceConstants.PasswordGrantType;
            if (valid) return;
            
            errors.AddFirst(new ValidationResult($"Invalid grant type: {GrantType}"));
        }
    }
}