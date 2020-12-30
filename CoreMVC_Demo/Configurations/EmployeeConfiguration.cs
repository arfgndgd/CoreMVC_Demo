using CoreMVC_Demo.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.Configurations
{
    public class EmployeeConfiguration:BaseConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.EmployeeProfile).WithOne(x => x.Employee).HasForeignKey<EmployeeProfile>(x => x.ID);
        }
    }
}
