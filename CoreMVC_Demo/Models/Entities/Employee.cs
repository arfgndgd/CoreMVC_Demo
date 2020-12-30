using CoreMVC_Demo.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }

        public UserRole UserRole { get; set; }
        //Relational Properties
        public virtual IList<Order> Orders { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
    }
}
