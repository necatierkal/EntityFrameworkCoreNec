


using Microsoft.EntityFrameworkCore;
using ReadDemo;
using ReadDemo.Contexts;
using ReadDemo.Entities;
using System.Runtime.InteropServices;

//SelectDemo1();

//SelectDemo2();

//SelectDemo3();


//WhereDemo1();


//WhereLikeDemo();

//WhereDemo3();

//WhereDemo4();

//WhereDemo5();

//OrderByDemo1();

//OrderByDemo2();

//GroupByDemo1();

//GrroupByDemo2();
//GroupByDemo3();
//DistinctDemo1();


//HavingDemo();


//ComboOrnekDemo();



/*
 * 
T-SQL 
TOP
OFFSET
.
.
.

SQL standardında
MIN
MAX
AVG
SUM
COUNT

 */
//TopDemo();


//OffSetDemo();


//AggregationDemo();




/*
First()            ------> Birden fazla kayıt varsa ilkini döner, hiç kayıt dönmezse hata verir.
FirstorDefault()   ------> Birden fazla kayıt varsa ilkini döner, hiç kayıt dönmezse default değer atar.
Last()             ------> Birden fazla kayıt varsa sonuncusunu döner, hiç kayıt dönmezse hata verir.
LastorDefault()    ------> Birden fazla kayıt varsa sonuncusunu döner, hiç kayıt dönmezse default değer atar.
Single()           ------> Bir kayıt dönerse onu verir, birden fazla kayıt dönerse ya da hiç kayıt dönmezse hata verir.
SingleorDefault()  ------> Bir kayıt dönerse onu verir, birden fazla kayıt dönerse hata verir, ya da hiç kayıt dönmezse default değer atar.
 */

//FirstLastSingleDemo();


//AnyCountAllDemo();


//InnerJoinDemo();
//InnerJoinDemo2();


using (var context = new NorthwindContext())
{
    //var custId = "ALFKI";    
    //var customers = context.Database.ExecuteSqlRaw("update customers set ContactName=Salih Demiroğ where CustomerId={0}",custId); 
    //ExecuteSqlRaw ile sql sorgusunu direkt yazabiliriz, parametrik gösterime dikkat edilmeli. + ile bölüp değişkeni araya yazrsak sql injection yapılabilir.
    //var customers2 = context.Database.ExecuteSqlInterpolated($"update customers set ContactName=Salih Demiroğ where CustomerId={0}", custId);
    //var customers2 = context.Customers.FromSqlInterpolated($"select * from Customers where Country = 'Germany'").ToList();
    //var city = "Madrid";
    //var customers2 = context.Customers.
    //    FromSqlInterpolated($"select * from Customers where Country = {city}")
    //    .Where(x=>x.Country == "Spain").ToList();
    //FromSqlInterpolatd Iquaryable dır. Execute etmek için tolist kullandık. Sonuç üzerinden sorgulama yapılabilir. (Where, order by eklenebilir.)
    //Uzun sorgular olmadıkça kullanılmaması gerekir. Uzun sorgular da View olarak eklenebilir linq ya da lambda expression ile sorgulanabilir.
    //Fluent mapping ile view alınırsa maplanirken hasno key seçilirse viewlar yöneilebilir. Bu özellik EntityFramework Core da mevcut.

    context.Database.ExecuteSqlRaw(@"create proc GetProducts @categoryId int   //Birden fazla satır yazacağımız için @ koyduk başına.
                                     as begin
                                     select * from Products where CategoryId = @categoryId
                                     end");

    var categoryId = 1;
    var products = context.Products.FromSqlInterpolated($"Exec dbo.GetProducts {categoryId}"); //Dönen datayı Produts DbSetine set ettik.
}


Console.ReadLine(); 

Console.WriteLine("Bitti");

#region Select Sorgular

static void SelectDemo1()
{
    using (var context = new NorthwindContext())
    {
        var productList = context.Products.ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void SelectDemo2()
{
    using (var context = new NorthwindContext())
    {
        List<string> productNames = context.Products.Select(x => x.ProductName).ToList();

        foreach (var product in productNames)
        {
            Console.WriteLine(product);
        }
    }
}

static void SelectDemo3()
{
    using (var context = new NorthwindContext()) //Select sorgusunun içerisine anonim tip olarak select te istediğimiz kolonları yazdık. Complex bir tip oluşturmaya gerek kalmadı.
    {
        var products = context.Products.Select(t => new //anonim tip 
        {
            Id = t.ProductId, //Id yazmasaydık t.ProductId tek başına kalabilirdi.
            Adi = t.ProductName
        }).ToList();

        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}->{product.Adi}");
        }
    }
}


#endregion


#region Where Koşullu Sorgular

static void WhereDemo1()
{
    using (var context = new NorthwindContext())
    {
        //Sorgular Iqueryable dan IEnumarable a döndüğünde çalışır. ToList, FirstorDefault, SingleorDefault gibi kullanımlar bu dönüşümü sağlar.
        //var productList = context.Products.ToList().Where(x => x.UnitPrice > 100); //Tüm veriyi çeker sonra süzer
        var productList = context.Products.Where(x => x.UnitPrice > 100).ToList(); //select * from Products where UnitPrice>100 sorgusunu veri tabanına yollayıp bunun sonucunu döner.

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void WhereLikeDemo()
{
    using (var context = new NorthwindContext())
    {
        var productList = context.Products.Where(x => x.ProductName.StartsWith("Ch")).ToList();
        var productList2 = context.Products.Where(x => x.ProductName.EndsWith("a")).ToList();
        var productList3 = context.Products.Where(x => x.ProductName.Contains("b")).ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void WhereDemo3()
{
    using (var context = new NorthwindContext())
    {
        //var productList = context.Products.Where(x => x.UnitPrice<50 && x.UnitsInStock==0).ToList();  
        var productList = context.Products.Where(x => x.CategoryId == 1 || x.CategoryId == 3).ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void WhereDemo4()
{
    using (var context = new NorthwindContext())
    {
        var categoryIds = new List<int> { 1, 3, 5, 7, 9 };
        var productList = context.Products.Where(x => categoryIds.Contains(x.CategoryId)).ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void WhereDemo5()
{
    using (var context = new NorthwindContext())
    {

        var productList = context.Products.Where(x => (x.UnitPrice < 20 && x.UnitsInStock < 20) || (x.UnitPrice > 50 && x.UnitsInStock > 50)).ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}



#endregion


#region OrderBy Sorguları
static void OrderByDemo1()
{
    using (var context = new NorthwindContext())
    {

        var productList = context.Products.OrderBy(x => x.ProductName).ToList();
        var productList2 = context.Products.OrderByDescending(x => x.ProductName).ToList();

        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}

static void OrderByDemo2()
{
    using (var context = new NorthwindContext())
    {

        var productList = context.Products.OrderBy(x => x.UnitsInStock)
            .ThenBy(x => x.ProductName) //ThenByDescending de var.
            .ToList();


        foreach (var product in productList)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.UnitPrice}");
        }
    }
}


#endregion

#region Group By Sorguları

static void GroupByDemo1()
{
    using (var context = new NorthwindContext())
    {

        var countries = context.Customers.GroupBy(x => x.Country).Select(x => x.Key) // Entity frmaework grupladığı alanları key propertisine set eder, buna erişmek için KEY keyword'ü kullanılır.
            .ToList();


        foreach (var country in countries)
        {
            Console.WriteLine(country);
        }
    }
}

static void GrroupByDemo2()
{
    using (var context = new NorthwindContext())
    {

        var datas = context.Customers.GroupBy(x => new { x.Country, x.City }) // İki veya daha fazla alan gruplamak etmek için anonim tip oluşturduk. 
        .Select(x => new
        {
            Sehir = x.Key.City,
            Ulke = x.Key.Country //Burada yalnızca Key deseydik aynı kolon adlarıyla grupladığımız iki kolonu da görebilirdik. Kendimiz kolon adlarını değiştirdik.
        })
        .ToList();


        foreach (var data in datas)
        {
            Console.WriteLine($"{data.Ulke}->{data.Sehir}");
        }
    }
}

static void GroupByDemo3()
{
    using (var context = new NorthwindContext())
    {

        var datas = context.Customers.GroupBy(x => new { x.Country, x.City })
        .Select(x => new
        {
            Ulke = x.Key.Country,
            x.Key.City, //Hepsine isim vermek zorunda değiliz.
            Toplam = x.Count() // Count alacaksak anonim tip kullanmak zorundayız. Group by ın içerisinde böyle bir kolon yok, yalnızca key kullansaydık iki kolonumuz olurdu.(city,Country)
        })
        .ToList();


        foreach (var data in datas)
        {
            Console.WriteLine($"{data.Ulke}->{data.City}->{data.Toplam}");
        }
    }
}


#endregion


#region Distinct Sorguları


static void DistinctDemo1()
{
    using (var context = new NorthwindContext())
    {

        var datas = context.Customers.Select(x => new { x.Country, x.City }).Distinct().ToList();
        //Distinct içerisine func almaz, o yüzden önce select yapıp seçtiğimiz alanlara göre distinct lemeliyiz.


        foreach (var data in datas)
        {
            Console.WriteLine($"{data.Country}->{data.City}");
        }
    }
}



#endregion


#region Having Sorgusu
static void HavingDemo()
{
    using (var context = new NorthwindContext())
    {

        var datas = context.Customers.GroupBy(x => x.Country).Where(x => x.Count() >= 10) //Having için ayrı bi keyword yoktur. Grupladıktan sonra yazarsak having gibi çalışır.
           .Select(x => new
           {
               Country = x.Key,
               Total = x.Count()

           }).ToList();



        foreach (var data in datas)
        {
            Console.WriteLine($"{data.Country}->{data.Total}");
        }
    }
}


#endregion


#region Genel Örnek

static void ComboOrnekDemo()
{
    using (var context = new NorthwindContext())
    {

        var datas = context.Customers
            .Where(x => x.CompanyName.Contains("a"))
            .GroupBy(x => new
            {
                x.Country,
                x.City
            })
           .Where(x => x.Count() >= 2)
           .Select(x => new
           {
               Ulke = x.Key.Country,
               Sehir = x.Key.City,
               Total = x.Count()

           })
           .OrderByDescending(x => x.Total)
           .ToList();

        //  select Country,City,count(*) as Total from Customers where CompanyName like '%a%' group by Country,City having count(*)>=2  order by Total desc 

        foreach (var data in datas)
        {
            Console.WriteLine($"{data.Ulke}->{data.Sehir}->{data.Total}");
        }
    }
}

#endregion


#region T-SQL

static void TopDemo()
{
    using (var context = new NorthwindContext())
    {

        //var datas = context.Products.Take(5).ToList(); //Take komutu sql deki top a karşılık gelir
        var datas = context.Products.Take(5).OrderByDescending(x => x.UnitPrice).ToList(); // Sırada take önce bile olsa tüm datayı getirip sıralar onra en pahalı beşi alır.

        //  select Country,City,count(*) as Total from Customers where CompanyName like '%a%' group by Country,City having count(*)>=2  order by Total desc 

        foreach (var data in datas)
        {
            Console.WriteLine($"{data.ProductId}->{data.ProductName}->{data.UnitPrice}");
        }
    }
}

static void OffSetDemo()
{
    using (var context = new NorthwindContext())
    {
        //OFFSET paging işlemlerinde kullanılır. İlk 5 kaydı atla sonraki kayıtları getir gibi

        var datas = context.Products.Skip(5).Take(10).ToList(); //İlk 5 kaydı atla,sonraki 10 kaydı getir. SQL de offset Skip in yaptığı işi yapar.


        foreach (var data in datas)
        {
            Console.WriteLine($"{data.ProductId}->{data.ProductName}->{data.UnitPrice}");
        }
    }
}


#endregion


#region Aggregation Sorguları
static void AggregationDemo()
{
    using (var context = new NorthwindContext())
    {

        //Max,Min,Sum,Average,Count

        decimal enPahaliUrun = context.Products.Max(x => x.UnitPrice);
        decimal enUcuzUrun = context.Products.Min(x => x.UnitPrice);
        int toplamStokAdedi = context.Products.Sum(x => x.UnitsInStock);
        var ortalamaUrunFiyati = context.Products.Average(x => x.UnitPrice);
        var toplamUrunAdedi = context.Products.Count();
        var stoguBulunmayanUrunAdedi = context.Products.Count(x=>x.UnitsInStock==0);


        Console.WriteLine($"En Pahalı Ürün Fiyat : {enPahaliUrun}");
        Console.WriteLine($"En Ucu Ürün Fiyat : {enUcuzUrun}");
        Console.WriteLine($"Ortalama Ürün Fiyat : {ortalamaUrunFiyati}");
        Console.WriteLine($"Toplam Stok Miktarı : {toplamStokAdedi}");
        Console.WriteLine($"Toplam Ürün Adedi : {toplamUrunAdedi}");
        Console.WriteLine($"Stoğu Bulunmayan Ürün Adedi : {stoguBulunmayanUrunAdedi}");

    }
}


#endregion


#region FirstLastSingle Sorgular
static void FirstLastSingleDemo()
{
    using (var context = new NorthwindContext())
    {



        //First()
        //FirstorDefault()
        //Last()
        //LastorDefault()
        //Single()
        //SingleorDefault()

        //Customer customer = context.Customers.First();
        //Customer customer = context.Customers.First(x=>x.CustomerId=="ANTON");

        //Customer customer = context.Customers.FirstOrDefault(x=>x.CustomerId=="ANTON");
        //Customer customer = context.Customers.Last(x=>x.Country=="Germany");
        //Customer customer = context.Customers.LastOrDefault(x=>x.Country=="Germany2");
        //Customer customer = context.Customers.Single(x=>x.CustomerId=="ALFKI");
        Customer customer = context.Customers.SingleOrDefault(x => x.CustomerId == "ALFKI2");
        Customer customer2 = context.Customers.Where(x => x.CustomerId == "ALFKI").ToList()[0];
        //Yukarıdakiler obje döndü. Where ile yazmak istersek list döner, objeye çevirmek için list e çevirip 0ncı indeksini alırız.

    }
}


#endregion


#region AnyAll Sorguları
static void AnyCountAllDemo()
{
    using (var context = new NorthwindContext())
    {
        bool isExist = context.Customers.Any(x => x.CustomerId == "SLD");
        bool isExist2 = context.Customers.Count(x => x.CustomerId == "SLD") > 0;
        bool tumUrunlerBirLiradanPahaliMi = context.Products.All(x => x.UnitPrice > 2); // Tüm ürünlerin fiyatı 2 den büyükse true döner.

        Console.WriteLine($"SLD Müşterisi Var Mı: {isExist}");
        Console.WriteLine($"SLD Müşterisi Var Mı: {isExist2}");
        Console.WriteLine($"Tüm Ürünler Bir Liradan Pahalı Mı: {tumUrunlerBirLiradanPahaliMi}");
    }
}

#endregion

#region Joinler

static void InnerJoinDemo()
{
    using (var context = new NorthwindContext())
    {
        var products = context.Products.Join(context.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new // 4 adet parametre set ettik
                                                                                                                     //ilki neyle joinleneceği iki ve üç neyle joinleneceği dört nerelerin select edileceği (anonim tip olarak )
                                                                                                                     //Sadece products ın tamamını seçseydik new yerine p yazacaktık.
        {
            p.ProductId,
            p.ProductName,
            c.CategoryName,
            p.UnitPrice
        });

        foreach (var product in products)
        {
            Console.WriteLine($"{product.ProductId}->{product.ProductName}->{product.CategoryName}");
        }
    }
}

static void InnerJoinDemo2()
{
    using (var context = new NorthwindContext())
    {
        //Anonim tip yerine custom tipte select yapıyor. Return edilecekse böyle yapmak zorundayız. Fonksiyon voidse anonim tip kullanılabilir.
        //Join yerine GroupJoin kullanılırsa left join yapar.
        var products = context.Products.Join(context.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new ProductDTO
        {
            Id = p.ProductId,
            Name = p.ProductName,
            CategoryName = c.CategoryName,
            UnitPrice = p.UnitPrice
        });

        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}->{product.Name}->{product.CategoryName}");
        }
    }
}
#endregion






