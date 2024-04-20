namespace RelationDemo.Entities;

public class OgrenciDetay
{
    public Guid OgrenciId { get; set; }
    public string? Adres { get; set; }
    public string? Telefon { get; set; }
    public string? DogumYeri { get; set; }

    public Ogrenci Ogrenci { get; set; } = null!;
}
