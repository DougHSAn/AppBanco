using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBanco.Models
{
    public class Account
    {
        public int NumAccount { get; set; }
        public float Balance { get; set; }
        public List<AccTran> AccTran { get; set; }
    }
}
