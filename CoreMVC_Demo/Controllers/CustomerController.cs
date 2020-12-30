using CoreMVC_Demo.Models.Context;
using CoreMVC_Demo.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Cart
        }
    }
}
