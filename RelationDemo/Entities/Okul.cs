namespace RelationDemo.Entities;

public class OgretimDuzeyi
{
    public int Id { get; set; }
    public string Seviye { get; set; } = null!;
}

public class Okul
{
    public int Id { get; set; }
    public int OgretimDuzeyiId { get; set; }
    public string Ad { get; set; } = null!;
    public string Adres { get; set; } = null!;

    public OgretimDuzeyi OgretimDuzeyi { get; set; } = null!;
    public ICollection<Ogrenci>? Ogrenciler { get; set; }
}