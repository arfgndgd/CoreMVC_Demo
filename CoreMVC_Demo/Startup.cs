using CoreMVC_Demo.Models.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            //Hangi servisin kullanýlacaðý(Ancak henüz kullanmadýk) Servisleri ekliyoruz sadece

            //Burada standart bir Sql baðlantýsý belirlemekk istersek (sýnýf içerisindeki optionBuilderdan belirlemektense bu tercih edilir) burada belirlemelisiniz

            //Pool kullanmak bir singletonPattern görevi görür
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //Baðlantý ayarýmýzý burada belirlemiþþ olduk. Context sýnýfýnda ayarýný yapmamýz lazým 
                      
            //Yukarýdaki ifadede dikkat ederseniz UseLazyLoadingProxies ifadesi kullanýlmýstýr. Bu durum .NetCore'daki Lazy Loading'in sürekli tetiklenebilmesi adýna environment'inizi garanti altýna almanýzý saglar.


            //***Önemli: Authentication iþlemini yapabilmek için servisi burada yaratmak gerekir.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.LoginPath = "/Home/Login";

            });


            //session kullanacak iseniz ayarlamalarýný yapmayý sakýn unutmayýn
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20); //Alýþveriþ boþ durduðunda ne kadar dursun zamanlamasý
                x.Cookie.HttpOnly = true; //Protokol güvenliði
                x.Cookie.IsEssential = true; //Bu da güvenlikle ilgili
            });

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

            //Authenticationý Authorizationdan önce vermeye özen göstermek gerekir.
            app.UseAuthentication();//kullanýcý kim bunu algýlar
            
            app.UseAuthorization(); //Yetkimiz var mý yok mu yani durumlarýnda (Authorization) çalýþacak metotdur.

            //Session'ý ekledikten sonra kullanmayý unutmayacaðýz.
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
