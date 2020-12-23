using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public decimal UnitInPrice { get; set; }
        public short UnitInStock { get; set; }
        public int CategoryID { get; set; }

        //Relation Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
