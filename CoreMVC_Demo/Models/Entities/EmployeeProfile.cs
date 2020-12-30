using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class EmployeeProfile:BaseEntity
    {
        public string SpecialDetail { get; set; }
        //Relational Properties

        public virtual Employee Employee { get; set; }

    }
}
