using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Customer
    {
        public int Id { get; set; }
        public virtual List<Sale> Transactions { get; set; }
        public virtual Address CustAddress { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
    }
}
