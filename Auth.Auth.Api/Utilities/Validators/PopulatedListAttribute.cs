using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Auth.Api.Utilities.Validators
{
    public class PopulatedListAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return new ValidationResult("Array must not be null", 
                new[] {validationContext.DisplayName});

            var list = value as List<string>;
            if (list.Count == 0)
                return new ValidationResult("Array must not be empty", new[] {validationContext.DisplayName});
            
            return ValidationResult.Success;
        }
    }
}