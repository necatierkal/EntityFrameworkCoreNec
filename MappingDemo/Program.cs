





/*
 Entity Frameworkte mapping için iki yöntem vardır.
1-Fluent Mapping -- Ayrı bir sınıfta yapılır. Daha nesenledir.
2-Data Annotation -- Attribute ler kullanılır. (Category cs buna örnek olarak hazırlandı)


 */

using MappingDemo;

using (var context = new MsbStoreContext())
{
    context.Database.EnsureCreated();

    //var allProducts = context.Products.AsNoTracking().ToList();
    //var productsForAdmin = context.Products.IgnoreQueryFilters().ToList();


    //var product = context.Products.AsNoTracking().Single(t => t.Id == 1);
    //product.UnitPrice = 209;

    //var entry = context.Entry(product);
    //entry.State = EntityState.Added;


    //var product = context.Products.AsTracking().Single(t => t.Id == 1);

    //context.SaveChanges();
}

Console.WriteLine("DB İşlemi Tamamlandı");