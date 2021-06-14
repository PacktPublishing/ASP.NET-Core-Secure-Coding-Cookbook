using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBankingApp.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First Name must contain only letters")]
        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Middle Name must contain only letters")]
        [Display(Name = "Middle Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string MiddleName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last Name must contain only letters")]
        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [ReputableEmail]
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(9)]
        [Required]
        public string Phone { get; set; }
    }
}