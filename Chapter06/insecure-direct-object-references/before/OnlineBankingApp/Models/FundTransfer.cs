using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;

namespace OnlineBankingApp.Models
{
    public class FundTransfer : IValidatableObject
    {
        public int ID { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        [Display(Name = "Account From")]
        public Guid AccountFrom { get; set; }

        [Display(Name = "Account To")]
        public Guid AccountTo { get; set; }        
        
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]        
        public decimal Amount { get; set; }

        [StringLength(60)]
        [DataType(DataType.MultilineText)]
        public string Note  { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AccountFrom == AccountTo)
            {
                yield return new ValidationResult("The fund transfer destination must be different from the source");
            }
        }

    }
}