using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatement.Models
{
    public class Transaction
    {
        public Guid ID { get; set; }
        public DateTime DateTransaction { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Description { get; set; }
        public Guid AccountID { get; set; }
        public Account Account { get; set; }
    }
}
