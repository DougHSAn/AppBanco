using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBanco.Models
{
    public class Transaction
    {
        public int IDTransaction { get; set; }
        public int TransactionValue { get; set; }
        public int IDSourceAccount { get; set; }
        public int IDTargetAccount { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<AccTran> AccTran { get; set; }
    }
}
