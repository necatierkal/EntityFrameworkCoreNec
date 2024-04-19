using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace MappingDemo
{

    //Mappleme işlemi için Attributler kullandık.
    //Köşeli parantez içinde attribute yazılır. Attribute bir classtır, attribute nesnesinden inherit edilmiştir.
    //Yazılımda benzer türtdeki nesneleri bibirinden ayırt etmek için kullanılan bir özellikttir. Birbirinden ayırt etmek için bir özllik katıyoruz attribute vasıtasıyla.    
    //Herşeye attribute uygulanabilir.
    //Entity Framework reflection mantığıyla çalışır. Yani runtime da oluşur.

    [Table("Kategoriler",Schema ="dbo")] //Table attribute ine sahip class. Bu Nesnenin karşılığı Kategorilerdir dedik. Default şema verdik, değiştirilebilir.
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Bu alan identity olmasın dedik. None yerine identity yazarsak identity kolon olarak işaretler. Defaultu int olduğu için identity set eder.
        [Column("Id")]
        public int CategoryKey { get; set; } //Id yazmadığı için Primary key olarak görmez ve hata verir entity framework. Attribute ile belirtmek gerekir ya da hasnokey denmelidir.

        [Column("Ad")] //Kolonun adını set ettik.  
        public string Name { get; set; } = string.Empty;

        [Column("Açıklama")]
        [StringLength(250)] //Açıklama alanı maksimum karakter sayısı
        [Required] //Not null
        public string Description { get; set; }


        [NotMapped] //Bunu map etme demek
        public string UniqueData => $"{CategoryKey}_{Name[0]}";//Bu bir kolon değil bunu belirtmemiz gerek. Vt da olmayacak.

        public ICollection<Product>? Products { get; set; } 

    }
}
