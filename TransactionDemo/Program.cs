

using TransactionDemo.Contexts;
using TransactionDemo.Entities;

//The isolation levels defined by the ANSI/ISO SQL standard are listed as follows.
//Serializable.
//Repeatable reads.
//Read committed.
//Read uncommitted.

//TransactionDemo();




using (var context = new NorthwindContext())
{
    using (var transaction = context.Database.BeginTransaction())

    {
        try
        {
            var category = new Category
            {
                CategoryName = "Otomobil"
            };
            context.Categories.Add(category);
            context.SaveChanges();

            context.Products.Add(new Product
            {
                ProductName = "Mercedes",
                QuantityPerUnit = "acıklamankjbhfbh nvfjvbdfhfbudhfbuahuıfhba",
                UnitPrice = 25_000,
                UnitsInStock = 2,
                CategoryId = category.CategoryId

            });
            context.SaveChanges();
            transaction.Commit();

        }
        catch (Exception)
        {

            Console.WriteLine("Hata oluştu");//using olmasa transaction.dispose veya rollback kullaırdık
        }

    }

    //Bu örnekte eklenmesi kategorinin eklenmesine bağımlı product eklenirken hata alınması durumunda kategoriyi de geri aldık.(Using içerisinde trabsaction yazdık.
    //Using altında üç nokta var sebebi bitimi parent usingile aynı olduğu için yeni scope açmaya gerek olmamasıdır.
    //tüm işlemlerin üzerinde   using (var transaction = context.Database.BeginTransaction());  olarak kullanılailir bitimi parent scope un bitimi olur.)

    //using (var transaction = context.Database.BeginTransaction())
    //{
    //    var category = new Category
    //    {
    //        CategoryName = "Otomobil"
    //    };

    //    context.Categories.Add(category);
    //    context.SaveChanges();


    //    context.Products.Add(new Product
    //    {
    //        ProductName = "Mercedes",
    //        QuantityPerUnit = "açıklamaaaaaaaaaaa",
    //        UnitPrice = 25_00,
    //        UnitsInStock = 2,
    //        CategoryId = category.CategoryId //Buradaki category id Vt dan çekmememize rağmen dolu olarak görünür sebebi classların referans tip olması. 
    //    });

    //    context.SaveChanges();

    //}

}


using (var context = new NorthwindContext())
{
    //Bu örnekte eklemeler ayrı transactionlarda yönetiliyor.

   var category = new Category
    {
        CategoryName = "Mobilya"
    };

    context.Categories.Add(category);
    context.SaveChanges();


    context.Products.Add(new Product
    {
        ProductName = "Masa",
        QuantityPerUnit = "Ceviz ağacından",
        UnitPrice = 25_00,
        UnitsInStock = 2,
        CategoryId = category.CategoryId //Buradaki category id Vt dan çekmememize rağmen dolu olarak görünür sebebi classların referans tip olması. 
    });

    context.SaveChanges();
}

static void TransactionDemo()
{
    using (var context = new NorthwindContext())
    {
        context.Categories.Add(new Category
        {
            CategoryName = "Mobilya"
        });
        context.Categories.Add(new Category
        {
            CategoryName = "Elektronik"
        });
        context.Categories.Remove(new Category
        {
            CategoryId = 9
        });

        context.Customers.Add(new Customer() //Burası hatalı 
        {
            City = "Ankara",
            CompanyName = "SLD Yazılım",
            ContactName = "Salih DEMİROĞ",
            Country = "Türkiye",
            CustomerId = "ALFKI"
        });

        context.SaveChanges(); //Sonda kullanırsak tüm işlemleri tek transaction a alır. Hatadan dolayı diğerlerini de(tamamını) rollback yapar.
                               //Ayrı yönetmek istersek her add remove adımından sonra save changes demeliydik.

    }
}

