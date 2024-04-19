





/*
 Entity Frameworkte mapping için iki yöntem vardır.
1-Fluent Mapping -- Ayrı bir sınıfta yapılır. Daha nesenledir.
2-Data Annotation -- Attribute ler kullanılır. (Category cs buna örnek olarak hazırlandı)


 */

using MappingDemo;

using (var context = new MsbStoreContext())
{
    context.Database.EnsureCreated(); 
}

Console.WriteLine("DB İşlemi Tamamlandı");