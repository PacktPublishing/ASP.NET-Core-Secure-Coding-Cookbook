using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OnlineBankingApp.Models
{
    public class Knowledge
    {
        public int ID { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Sensitivity { get; set; }

    }
}