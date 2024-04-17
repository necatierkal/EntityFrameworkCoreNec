

using CreateUpdateDeleteDemo.Contexts;
using CreateUpdateDeleteDemo.Entities;

//AddDemo();

//AddDemo2();

//UpdateDemo();

//UpdateDemo2();


//DeleteDemo();

Console.WriteLine("Bitti");

#region Kayıt Ekleme Örnekleri

static void AddDemo()
{
    using (var context = new NorthwindContext())
    {
        var newCategory = new Category
        {
            CategoryName = "Gıda"
        };

        context.Categories.Add(newCategory);
        context.SaveChanges();
    }
}

static void AddDemo2()
{
    using (var context = new NorthwindContext())
    {
        var newProduct = new Product
        {
            CategoryId = 10,
            Discontinued = false,
            ProductName = "Ekmek",
            QuantityPerUnit = "200 gr kepekli ekmek",
            UnitPrice = 25,
            UnitsInStock = 20
        };

        context.Products.Add(newProduct);
        context.SaveChanges();
    }
}


#endregion

#region Kayıt Güncelleme Örnekleri


static void UpdateDemo()
{
    using (var context = new NorthwindContext())
    {
        var product = context.Products.Find(80); //Where gibi çalışıp ilgili kaydı id sinden buldu.
        product.UnitPrice = 35;
        context.Products.Update(product);
        context.SaveChanges();
    }
}

static void UpdateDemo2()
{
    using (var context = new NorthwindContext())
    {
        var product = context.Products.Find(80); //Where gibi çalışıp ilgili kaydı id sinden buldu.
        product.UnitPrice = 45;

        context.SaveChanges();
    }
}



#endregion

#region Kayıt Silme Örnekleri
static void DeleteDemo()
{
    using (var context = new NorthwindContext())
    {
        //var product = new Product
        //{
        //    ProductId = 80
        //};
        var product = context.Products.Find(80);
        context.Products.Remove(product);
        context.SaveChanges();
    }
}
#endregion