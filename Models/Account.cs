using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public float Balance { get; set; }
        public int CustomerID { get; set; }
    }
}
