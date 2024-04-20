namespace RelationDemo.Entities;

public class Ogrenci
{
    public Guid Id { get; set; }
    public int OkulId { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public DateTime DogumTarihi { get; set; }
    public DateTime KayitTarihi { get; set; }

    public OgrenciDetay? OgrenciDetay { get; set; }
    public Okul Okul { get; set; } = null!;
    public ICollection<Ders>? Dersler { get; set; }
}
