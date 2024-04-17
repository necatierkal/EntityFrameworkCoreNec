


using ReadDemo.Contexts;

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