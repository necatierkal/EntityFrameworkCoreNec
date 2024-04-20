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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MsbStoreMapping; Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Urunler", "dbo");

            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            //modelBuilder.Entity<Product>().HasNoKey(); //Heap tablo (PK yok) ise

            modelBuilder.Entity<Product>().Property(t => t.Name)
                .HasColumnName("Ad")
                .IsRequired();

            modelBuilder.Entity<Product>().Property(t => t.Description)
                .HasColumnName("Aciklama")
                .HasColumnType("varchar")
                .HasMaxLength(250);

            modelBuilder.Entity<Product>().HasQueryFilter(t => !t.IsDeleted);

            modelBuilder.Entity<Product>().HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);

            base.OnModelCreating(modelBuilder);
        }
    }
}
