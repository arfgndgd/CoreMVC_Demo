using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string Descripton { get; set; }

        //Relational Properties
        public virtual List<Product> Products { get; set; }
    }
}
