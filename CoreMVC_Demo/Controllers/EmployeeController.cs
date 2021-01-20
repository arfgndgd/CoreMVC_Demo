using CoreMVC_Demo.Models.Context;
using CoreMVC_Demo.Models.Entities;
using CoreMVC_Demo.VMClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Controllers
{
    //Görevlendirme yapacağız mesela yeni kullanıcı eklerken yetkisi ne olacak gibi
    //Burada ise amaç dropdownlist kullanarak UserRole enumından seçim yaptıracağız

    //[Authorize(Roles = "Admin")]
    //Hangi controllera koyar isek onda Yetkilendirme yapar. Direkt namespace koyarsak listlemeye bile ulaşamayız
    public class EmployeeController : Controller
    {
        MyContext _db;
        public EmployeeController(MyContext db)
        {
            _db = db;
        }
        public IActionResult EmployeeList()
        {
            EmployeeVM evm = new EmployeeVM
            {
                Employees = _db.Employees.ToList()
            };
            return View(evm);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        public IActionResult UpdateEmployee(int id)
        {
            EmployeeVM evm = new EmployeeVM
            {
                Employee = _db.Employees.Find(id),
            };
            return View(evm);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            Employee toBeUpdated = _db.Employees.Find(employee.ID);
            toBeUpdated.FirstName = employee.FirstName;
            toBeUpdated.UserRole = employee.UserRole;
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public IActionResult DeleteEmployee(int id)
        {
            _db.Employees.Remove(_db.Employees.Find(id));
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

    }
}
