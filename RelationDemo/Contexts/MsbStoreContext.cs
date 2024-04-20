using Microsoft.EntityFrameworkCore;
using RelationDemo.Entities;

namespace RelationDemo.Contexts;

public class MsbStoreContext : DbContext
{
    public DbSet<Ogrenci> Ogrenciler { get; set; }
    public DbSet<OgrenciDetay> OgrenciDetaylari { get; set; }
    public DbSet<Okul> Okullar { get; set; }
    public DbSet<Ders> Dersler { get; set; }
    public DbSet<OgretimDuzeyi> OgretimDuzeyleri { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Msu; Integrated Security=true;");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OgrenciDetay>().HasKey(t => t.OgrenciId);

        modelBuilder.Entity<Ogrenci>()
            .HasOne(t => t.OgrenciDetay)
            .WithOne(t => t.Ogrenci)
            .HasForeignKey<OgrenciDetay>(t => t.OgrenciId);

        modelBuilder.Entity<Ogrenci>()
            .HasOne(t => t.Okul)
            .WithMany(t => t.Ogrenciler)
            .HasForeignKey(t => t.OkulId);

        modelBuilder.Entity<Ogrenci>()
            .HasMany(t => t.Dersler)
            .WithMany(t => t.Ogrenciler)
            .UsingEntity("OgrenciDersleri",
            l => l.HasOne(typeof(Ders)).WithMany().HasForeignKey("DersId"),
            r => r.HasOne(typeof(Ogrenci)).WithMany().HasForeignKey("OgrenciId"),
            j => j.HasKey("DersId", "OgrenciId"));


        base.OnModelCreating(modelBuilder);
    }
}
