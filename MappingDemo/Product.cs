using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingDemo
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }

        public Category Category { get; set; } = null!; // navigation property vt da bir kolonu temsil etmez. İlişkiyi kurmak için yaptık.

    }
}
