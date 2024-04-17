using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo
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

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Product>? Products { get; set; } // One-toMany ilişki olduğu için Category (One) tek bir nesne olarak Product (Many) ise Icollection olarak belirtildi.
        //Bir kategorinin birden fazla ürünü vardır.
        //List olarak yazılmamalı, EF Icollection olarak istiyor.
        //İsimlendirme standırdına uyulmazsa mapping yapılmalı. CategoryId yerine KategoriId dersem custom mapping gerekli.
        
    }
}
