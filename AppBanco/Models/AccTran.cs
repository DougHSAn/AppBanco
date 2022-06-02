using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBanco.Models
{
    public class AccTran
    {
        public int IDTransaction { get; set; }
        public Transaction Transaction { get; set; }
        public int NumAccount { get; set; }
        public Account Account { get; set; }
    }
}
