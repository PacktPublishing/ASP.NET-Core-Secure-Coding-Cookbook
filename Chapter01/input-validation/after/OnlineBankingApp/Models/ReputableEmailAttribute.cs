using System.ComponentModel.DataAnnotations;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Models
{
    public class ReputableEmailAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => "Email address is rejected because of its reputation";
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            string email = value.ToString();
            var service = (IEmailReputation) validationContext.GetService(typeof(IEmailReputation));            
            if (service.IsRisky(email))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }
    }

    public class Details
    {
        public bool blacklisted { get; set; }
        public bool malicious_activity { get; set; }
        public bool malicious_activity_recent { get; set; }
        public bool spam { get; set; }
        public bool suspicious_tld { get; set; }
    }

    public class Reputation
    {
        public Details details { get; set; }
        public string email { get; set; }
        public string reputation { get; set; }
        public bool suspicious { get; set; }
    }    

}