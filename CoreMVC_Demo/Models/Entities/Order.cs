using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class Order:BaseEntity
    {
        public string ShipAddress { get; set; }
        public int EmployeeID { get; set; }

        //Relational Properties

        public virtual Employee Employee { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
