using Microsoft.EntityFrameworkCore;
using RelationDemo.Contexts;
using RelationDemo.Entities;

using (var context = new MsbStoreContext())
{
    context.Database.EnsureCreated();

    Guid salihId = Guid.Parse("F75EA698-36C7-48F8-88F0-FB09A91DECEE");
    Guid enginId = Guid.Parse("C480F470-EE19-45CE-A6FA-0A9D4360EB59");

    //var ogrenci1 = new Ogrenci
    //{
    //    Id = salihId,
    //    Ad = "Salih",
    //    Soyad = "Demiroğ",
    //    KayitTarihi = DateTime.Now,
    //    DogumTarihi = DateTime.Now.AddYears(-38),
    //    Okul = new Okul
    //    {
    //        Ad = "Gazi Üni",
    //        Adres = "Ankara",
    //        OgretimDuzeyi = new OgretimDuzeyi
    //        {
    //            Seviye = "Lisans"
    //        }
    //    },
    //    OgrenciDetay = new OgrenciDetay
    //    {
    //        Adres = "Batıkent",
    //        DogumYeri = "Diyarbakır"
    //    },
    //    Dersler = new List<Ders>
    //    {
    //        new Ders
    //        {
    //            Ad="Türkçe",
    //            KrediNotu=3,
    //        },
    //        new Ders
    //        {
    //            Ad="Matematik",
    //            KrediNotu=3,
    //        }
    //    }
    //};

    //var ogrenci2 = new Ogrenci
    //{
    //    Id = enginId,
    //    Ad = "Engin",
    //    Soyad = "Demiroğ",
    //    KayitTarihi = DateTime.Now,
    //    DogumTarihi = DateTime.Now.AddYears(-40),
    //    Okul = new Okul
    //    {
    //        Ad = "Başkent Üni",
    //        Adres = "Ankara",
    //        OgretimDuzeyi = new OgretimDuzeyi
    //        {
    //            Seviye = "Yüksek Lisans"
    //        }
    //    },
    //    OgrenciDetay = new OgrenciDetay
    //    {
    //        Adres = "Ümitköy",
    //        DogumYeri = "Diyarbakır"
    //    },
    //    Dersler = new List<Ders>
    //    {
    //        new Ders
    //        {
    //            Ad="Fen",
    //            KrediNotu=3,
    //        },
    //        new Ders
    //        {
    //            Ad="Cografya",
    //            KrediNotu=2.5m,
    //        }
    //    }
    //};

    ////context.Ogrenciler.Add(ogrenci1);
    ////context.Ogrenciler.Add(ogrenci2);

    //context.Ogrenciler.AddRange(ogrenci1, ogrenci2);
    //context.SaveChanges();


    var ogrenci = context.Ogrenciler
        .Include(t => t.OgrenciDetay)
        .Include(t => t.Okul)
        .ThenInclude(t => t.OgretimDuzeyi)
        .Include(t => t.Dersler.Where(x => x.KrediNotu < 3))

        .SingleOrDefault(t => t.Id == enginId);



}
