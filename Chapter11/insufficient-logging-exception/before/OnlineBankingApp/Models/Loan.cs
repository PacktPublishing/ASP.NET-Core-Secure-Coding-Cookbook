using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OnlineBankingApp.Models
{
    public class Loan
    {
        public int ID { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]        
        public decimal Amount { get; set; }

        [Display(Name = "Term in Months")]
        public int PeriodInMonths { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        public LoanStatus Status { get; set; }

        [Display(Name = "Notes")]
        public string Note { get; set; }
    }

    public enum LoanStatus
    {
        Approved,
        Denied,
        Pending
    }

}