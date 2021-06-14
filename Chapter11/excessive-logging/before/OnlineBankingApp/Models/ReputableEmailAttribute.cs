using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Models
{
    public class ReputableEmailAttribute : ValidationAttribute
    {
        public string Email { get; }

        public string GetErrorMessage() =>
            $"Email address is from a shady domain";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            string email = value.ToString();
            string tld = email.Substring(email.LastIndexOf('.') + 1);		
            string[] shadyTlds = { "country", "kim", "science", "gq", "work", "ninja", "xyz", "date", "faith", "zip", "racing", "cricket", "win", "space", "accountant", "realtor", "top", "stream", "christmas", "gdn", "mom", "pro", "men" };

            if(shadyTlds.Any(tld.Contains))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }

}