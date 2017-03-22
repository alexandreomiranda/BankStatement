using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatement.Models
{
    public class TransactionViewModel
    {
        public Guid ID { get; set; }
        public DateTime DateTransaction { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Description { get; set; }

        public string YearAndMonth { get; set; }
        public decimal Value { get; set; }
        
        public decimal StartingDayBalance { get; set; }
        public string Number { get; set; }
        
    }
}