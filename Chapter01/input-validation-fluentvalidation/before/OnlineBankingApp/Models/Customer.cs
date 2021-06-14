using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBankingApp.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]        
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(9)]
        [Required]
        public string Phone { get; set; }
    }
}