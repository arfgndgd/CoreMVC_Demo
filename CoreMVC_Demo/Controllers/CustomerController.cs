using CoreMVC_Demo.Models.Context;
using CoreMVC_Demo.Tools;
using CoreMVC_Demo.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC_Demo.CommonTools;
using CoreMVC_Demo.Models.Entities;

namespace CoreMVC_Demo.Controllers
{
    public class CustomerController : Controller
    {
        //Takı controller ile bitmesede controller olarak algılar(Core)
        MyContext _db;

        public CustomerController(MyContext db)
        {
            _db = db;
        }

        public IActionResult ShoppingList()
        {
            ProductVM pvm = new ProductVM
            {
                Products = _db.Products.ToList()
            };
            return View(pvm);
        }

        public IActionResult AddToCart(int id)
        {
            //Extension metod kullanımı: Programa gömülü olan HttpContext.Session'ı kullanacağız. Ancak biz kendi Sessionlarımızı kullanmak istiyoruz (SetObject, GetObject). Bunları kullanmak için using eklemek lazım.  

            //İlk şart hep false olandır yani soru işaretine kadar yeni cart yarat. Null değilse Sessiondakini al demektir.
            Cart c = HttpContext.Session.GetObject<Cart>("scart") == null ? new Cart() : HttpContext.Session.GetObject<Cart>("scart");

            Product eklenecek = _db.Products.Find(id);
            CartItem ci = new CartItem
            {
                ID = eklenecek.ID,
                Name = eklenecek.ProductName,
                Price = eklenecek.UnitInPrice
            };

            c.SepeteEkle(ci);

            HttpContext.Session.SetObject("scart", c);
            return RedirectToAction("ShoppingList");
        }

        public IActionResult CartPage()
        {
            if (HttpContext.Session.GetObject<Cart>("scart")==null)
            {
                TempData["mesaj"] = "Sepetinizde ürün bulunmamaktadır";
                return View();
            }
            else
            {
                Cart c = HttpContext.Session.GetObject<Cart>("scart");
                return View(c);
            }

        }
    }
}
