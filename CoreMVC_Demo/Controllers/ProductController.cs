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
    public class ProductController : Controller
    {
        //Constructor bir classtan instance alındığında veya classtan miras alındığında tetiklenir 
        //Ancak actionlar tetiklendiğinde, tarayıcıda host/action olduğunda çalışmaz. İlk tetiklenmesi gereken Controllerdır.
        MyContext _db;
        public ProductController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ProductVM pvm = new ProductVM
            {
                Products = _db.Products.ToList(),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }
        public IActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _db.Products.Find(id),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            Product toBeUpdated = _db.Products.Find(product.ID);
            toBeUpdated.CategoryID = product.CategoryID;
            toBeUpdated.ProductName = product.ProductName;
            toBeUpdated.UnitInPrice = product.UnitInPrice;
            toBeUpdated.UnitInStock = product.UnitInStock;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
