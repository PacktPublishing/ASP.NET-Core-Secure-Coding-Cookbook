using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace OnlineBankingApp.Models
{
    public class Account
    {
        public Guid ID { get; set; }

        public Customer Customer { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(15)]
        public string Name { get; set; }

        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]        
        public decimal Balance { get; set; }
    }

    public enum AccountType {
        Savings,
        Checking
    }

}