using CoreMVC_Demo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.VMClasses
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
