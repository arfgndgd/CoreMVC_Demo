using CoreMVC_Demo.Models.Context;
using CoreMVC_Demo.Models.Entities;
using CoreMVC_Demo.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Controllers
{
    public class CategoryController : Controller
    {
        /*
        Interface segragation: interfacelerin ayrı ayrı görevleri tek bir çatı altında toplamaktansa sorumluluklarını daha fazla ayırmaktır. Mesela Repositoryleri açtığımızda her class için ayrı repository açarız. (ICategoryRepository, IProductRepository)
        Dependency Inversion: Bağımlılıkların azalması, gevşek tutulmasıdır.Interface segragation ile zaten sorumlulukları azalttık bu da dependency inversiona uyar.

        Dependency Inversion prensibini uygulamak için Dependency Injection dediğimiz tasarım paternini kullanırız.Bu pattern istediğimiz şekilde istediğimiz sorumluluğun hemen o an için enjekte edilmesini sağlayan bir tasarım paternidir.Dependecy Injection en rahat interface yapısı ile kullanılabilir, böylece istediğimiz an sorumluluğu değiştirebiliriz.
        */


        MyContext _db;

        //Singleton patterndeki DbToola ihtiyaç yok çünkü Startup.cs de Pool kullandık. Böylece herhangi bir metodda MyContext tipinde bir parametre görüldüğü anda otomatik olarak singleton pattern uygulanarak ramdeki yapı getirelecek

        public CategoryController(MyContext db)//kısmi bir dependency injection uygulanır çünkü interface kullanılmamıştır. İleride MyContext interface olabilir, database interface olarak kullanılabilir. Bunun nedeni databasein miras aldığı DbContext tamamen interfacelerden alınmıştır.
        {
            _db = db;
        }

        //.Net Core, MVC Helper'larinizi korumasının yanı sıra size daha kolay ve daha performanslı bir yapı da sunar...Bunlara Tag Helper'lar denir. Tag Helper'lar normal HTML taglerinin icerisine yazılan attribute'lardır. Kullanablimek icin namespace'leri gereklidir(Zaten _ViewImportsda vardır)

        //Projeyi watch run olarak izleyebilmek adına projenin klasorune gidip cmd ekranına girerek ilgili terminale dotnet watch run komutunu yazmak gerekir. Böylece projede yaptıklarını kaydettikten sonra çalışma anında sayfanızı refresh ederek degişiklikleri gözlemleyebilirsiniz.
        public IActionResult Index()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(cvm);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory([Bind(Prefix ="Category")] Category category)//property ismiyle parametre isminin tutmasıyla ilgili
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
