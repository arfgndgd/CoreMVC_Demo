using CoreMVC_Demo.Configurations;
using CoreMVC_Demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoreMVC_Demo.Models.Context
{
    public class MyContext:DbContext
    {
        //Modelin configurasyon ayarları, appsetting.json içindeki connectionString burada da yapılabilir,SqlServer indirmek gerekir.

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer(connectionString: "server = LENOVO-PC\\SQLEXPRESS;database=...;integrated security=true");
        //}


        //Dependency Injection yapısı Core platformuzunuzun arkasında otomatik olarak entegre gelir. Dolayısıyla siz bir veritabannı sınıfınızın constructtorına parametre olarak bir Option tipinde verirseniz bu parametreye argüman otomatik gönderilir
        
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {//Startup içinde ConfigureServis içinde Pool ayarını yaptık burada yapmamız lazım, DbContextOptions'ı temsil eder. Oradanda "MyConnection"ı options ile alır.

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ayrı bir katmanda değilde direkt yazmak istersek bu şekilde kullanabiliriz
            //modelBuilder.Entity<OrderDetail>().Property(x => x.ID).UseIdentityColumn();
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            base.OnModelCreating(modelBuilder);//base'i de korusun diye
        }

        //Core migration: add-migration <parametre(herhangi bir yazı)> ve sonrasında update-database 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<EmployeeProfile> Profile { get; set; }

    }


}
