using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.EntityFramework
{//database e bağlanıp bir tablodaki verileri alabilmek için bir context oluşturuyoruz bu context veritabanı ile program arasındaki bağlantıyı kurmamızı sağlıyor . 


    //Veritabanı bağlantılarını yönetir.
    //Model ve ilişkileri ayarlar.
    //Veritabanı sorgulama.Değişikleri yönetir.
    //CachingTransaction yönetimiObject Materialization (Veritabanından aldığı verileri entity nesnelerine dönüştürür).
    public class NorthwindContext :DbContext

    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }

    }
}
