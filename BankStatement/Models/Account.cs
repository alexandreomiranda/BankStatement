using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatement.Models
{
    public class Account
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
    }
}