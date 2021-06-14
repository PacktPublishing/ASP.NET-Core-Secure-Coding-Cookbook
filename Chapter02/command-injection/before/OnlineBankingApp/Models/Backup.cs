using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OnlineBankingApp.Models
{
    public class Backup
    {
        public int ID { get; set; }

        [Display(Name = "Backup Name")]
        [StringLength(15)]
        public string Name { get; set; }

        [Display(Name = "Backup Date")]
        [DataType(DataType.Date)]
        public DateTime BackupDate { get; set; }

    }
}