using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBankingApp.Models
{
    public class Account
    {
        public int ID { get; set; }

        //public int CustomerID { get; set; }
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