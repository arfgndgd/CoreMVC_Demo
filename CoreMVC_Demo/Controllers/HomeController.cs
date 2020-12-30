//using CoreMVC_Demo.Models;
using CoreMVC_Demo.Models.Context;
using CoreMVC_Demo.Models.Entities;
using CoreMVC_Demo.VMClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Controllers
{
    public class HomeController : Controller
    {
        MyContext _db;
        public HomeController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        //.Net Core Authorization İşlemleri

        /*
         Async metotlar her zaman generic bir Task döndürmek zorundadır. İstersek döndürülen Task'i kullanabilir istersek kulllanmayız ama döndürmek zorundayız.
        Task classı asenkron metotların çalışma prensipleri hakkkında ayrınıtlı bilgiyi tutan(Metot calısırken hata var mı,metodun bu görevi yapma sırasında kendisine eş zamanlı gelen istekler,metodun calısma durumu(success,flawed)) O yüzden normal şartlarda döndürecegimiz değeri Task'e generic olarak vermek zorundayız.

        Login Logout tıklamaları için SignInAsync, SignOutAsync kullandık

        Task = Görev
        */
        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {

            Employee loginEmployee = _db.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName);
            if (loginEmployee != null)
            {
                //Claim, rol bazlı veya identity bazlı güvenlik işlemlerinden sorumlu olan bir classtır. Dilersek birden fazla Claim nesnesi yaratıp hepsini aynı anda kullanabiliriz.

                /*Algoritması: 
                    1. Güvenlik önlemlerini elle ayarlamamız lazım (List<Claim>....)     
                    2. Bir Identity nesnesi oluşturarak bu önemleri kim için yapacağız (ClaimsIdentity)
                    3.Oluşşturulan güvenlik önlemini Claims Prensibi içine almamaız lazımm kii .net core anlasın (ClaimsPrincipal...)
                 */
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,loginEmployee.UserRole.ToString())//enum olduğu için stringe çevirdik
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login"); //login ismine sahip olan güvenlik durumu için hangii güvenlik önlemlerinin çalışacağını belirliyoruz.

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);//.Net Core'in icerisinde sonlanmıs olan security işlemlerinin artık tetiklenmesi lazım (yani login işleminin yapılması lazım)

                //asenkron metotlar calıstıkları zaman baska bir işlemin engellenmemesini saglayan metotlardır..(normal durumda metotlar sıra ile çalışır)

                //******
                //Eger bir async metot kullanıyorsak mecburi bu metodu cagıran yapıya async keywordunu verip IActionı "Task<>" içine almamız gerekir.
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Product");
            }
            return View(new EmployeeVM { Employee = employee });
    
        }
        public async Task<IActionResult> LogOut()
        {
            //Bu bir sayfa değil bir tıklama işlemidir.
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }

}
 