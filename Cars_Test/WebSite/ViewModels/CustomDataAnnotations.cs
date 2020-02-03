using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels
{

    public class CorrectYear : ValidationAttribute
    {
        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var now = (int)value;

            return (now <= DateTime.Now.Year && now >= 1900) ? ValidationResult.Success : new ValidationResult("Please choose correct year");
        }
    }
}
