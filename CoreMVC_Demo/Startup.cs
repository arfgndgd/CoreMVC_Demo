using CoreMVC_Demo.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Hangi servisin kullan�laca��(Ancak hen�z kullanmad�k) Servisleri ekliyoruz sadece

            //Burada standart bir Sql ba�lant�s� belirlemekk istersek (s�n�f i�erisindeki optionBuilderdan belirlemektense bu tercih edilir) burada belirlemelisiniz

            //Pool kullanmak bir singletonPattern g�revi g�r�r
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"))); //Ba�lant� ayar�m�z� burada belirlemi�� olduk. Context s�n�f�nda ayar�n� yapmam�z laz�m 


            //Authentication i�lemi

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=EmployeeList}/{id?}");
            });
        }
    }
}
