using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingDemo
{
    public class MsbStoreContext : DbContext // Oluşturulan tablolar yani cs dosyalarını EF'e bir veri tabanı nesnesi (entity) olarak set etmesi için burada bildiriyoruz.
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Nuget paketi indirerek connection string i belirtiyoruz.
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MsbStoreMapping; Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
