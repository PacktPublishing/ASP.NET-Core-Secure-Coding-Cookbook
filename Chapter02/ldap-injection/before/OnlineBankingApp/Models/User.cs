using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Models
{
    public class User
    {
        [Display(Name = "UID")]
        public string UserName { get; set; }
        
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}