using CoreMVC_Demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Configurations
{
    public class OrderDetailConfiguration:BaseConfiguration<OrderDetail>
    {
        //Sql veritabanı için ayarlama yapmak istiyoruz.
        //Lazy Loading ile overrideın baseteki ayarlamasını ezmeyi önlemiş oluruz. Gidip BaseConfiguration içinde implementle gelen yapıya virtualı eklersek override ile yeni bir seçenek gelir.

        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);//base'i olduğu gibi bırakıyoruz böylece o özelliği de alır
            builder.Ignore(x => x.ID);
            builder.HasKey(x => new { x.OrderID, x.ProductID });
            builder.ToTable("Satışlar");
        }
    }
}
