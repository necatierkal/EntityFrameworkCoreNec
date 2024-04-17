


using FirstDemo;





//var context = new MsbStoreContext();
//context.Database.EnsureCreated(); //Database var ve içeriği doluysa(en az bir tablo  varsa) birşey yapmaz,
//                                  //eğer database var ve hiç tablo yoksa entity framework modeli kullanarak database i oluşturur. (EnsureCreated)

//context.Dispose(); //Bağlantı sonlandırılıp nesne serbest bırakıldı(null a çekildi).

//Dispose etmek yerine using ile bağlantı açılabilir. Using bizim için bunu yönetir.
//Bellek dışında veri okunup yazılan herşey yönetilmeyen kaynaklardır. Disposeble dır hepsi aşağıdaki gibi yönetilebilir.

using (var context = new MsbStoreContext())
{
    context.Database.EnsureCreated(); //Database var ve içeriği doluysa(en az bir tablo  varsa) birşey yapmaz,
                                      //eğer database var ve hiç tablo yoksa entity framework modeli kullanarak database i oluşturur. (EnsureCreated)
}

Console.WriteLine("DB İşlemi Tamamlandı");