using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDemo.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
