using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Models.Entities
{
    public class OrderDetail:BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        //Relational Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        //Çoka çok ilişkide.NetFramework'te MAP katmanında yaptığımız EntityTypeConfiguration'a karşılık gelen ayarlamaları CoreMVCde farklı bir yerde yaparız.Ancak Interface üzerinden ayarlamaları yapacağımız için MAPteki gibi constructor içinde yapamayız çünkü interfacelerin constructorı yoktur.
    }
}
