


using LinqDemo.Contexts;
using Microsoft.Identity.Client;

using (var context = new NorthwindContext())
{
    //Linq sorguları
    //var products = (from p in context.Products //select en sona yazılır
    //               select p).ToList();
    //var products = (from p in context.Products //select en sona yazılır
    //                select p.ProductName).ToList();

    //var products = (from p in context.Products //select en sona yazılır
    //                select new
    //                {
    //                    p.ProductId,
    //                    p.ProductName
    //                }).ToList();

    //var products = (from p in context.Products
    //                where p.CategoryId == 1
    //                select p)
    //                .ToList();

    //var products = (from p in context.Products
    //                where p.CategoryId == 1
    //                orderby p.ProductName //Birden fazla order verilmek istenirse anonim tip yapılır new {p.ProductName,p.ProductId} gibi
    //                select p)
    //               .ToList();

    //var products = (from c in context.Customers
    //                group c by c.Country into g //Birden fazla gruplama ihtiyacında yine anonim tip yapılır.
    //                select g.Key).ToList();



    //var datas = (from c in context.Customers
    //                group c by new { c.Country,c.City } into g //Birden fazla gruplama ihtiyacında yine anonim tip yapılır.
    //                select new
    //                {
    //                    g.Key.Country,
    //                    g.Key.City,
    //                    Total = g.Count()
    //                }).ToList();

    //var datas = (from c in context.Products                
    //             select c.UnitPrice).Max();  //Max fonksiyonu linq da yok. Sonucun maxını alabiliriz. 



    //var datas = (from p in context.Products
    //             where p.ProductId == 3
    //             select p.UnitPrice).SingleOrDefault(); //tek kayıt almak için.
    ////Top komutu istiyorsak yine Take yazarız.
    ///


    //var products = (from p in context.Products 
    //               join c in context.Categories
    //               on p.CategoryId equals c.CategoryId
    //               select new
    //               {
    //                   p.ProductId,
    //                   p.ProductName,
    //                   c.CategoryName
    //               }).ToList();


    //var products = (from p in context.Products
    //                join c in context.Categories
    //                on new { p.CategoryId, Name = p.ProductName } equals new { c.CategoryId, Name = c.CategoryName } //Örnek olsun diye yazdık.
    //                //join şartı birden fazla olursa bu şekilde yazılır. Eşleşen kolonlar aynı adda olmak zorundadır.
    //                select new
    //                {
    //                    p.ProductId,
    //                    p.ProductName,
    //                    c.CategoryName
    //                }).ToList();




    //var products = (from p in context.Products
    //                join c in context.Categories on p.CategoryId   equals  c.CategoryId
    //                join m in context.Customers  on c.CategoryName equals m.CustomerId                    
    //                select new
    //                {
    //                    p.ProductId,
    //                    p.ProductName,
    //                    c.CategoryName,
    //                    m.CompanyName
    //                }).ToList();


    var products = (from p in context.Products
                    join c in context.Categories on p.CategoryId equals c.CategoryId into ps  
                    //Left join olması için equalstn sonra into, unique alias ve from join alias ile birlikte into dan sonraki alias ve defaultifempty kullanılır.
                    from c in ps.DefaultIfEmpty()
                    select new
                    {
                        p.ProductId,
                        p.ProductName,
                        c.CategoryName                   
                    }).ToList();




}