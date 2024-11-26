using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var emailPattern = @"^[^@\s]+@gmail\.com$";
            if (Regex.IsMatch(value.ToString(), emailPattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid email format.");
            }
        }
    }
}
