using CoreMVC_Demo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.VMClasses
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
