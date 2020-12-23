using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
