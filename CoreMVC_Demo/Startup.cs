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

            //Hangi servisin kullan�laca��(Ancak hen�z kullanmad�k) Servisleri ekliyoruz sadece

            //Burada standart bir Sql ba�lant�s� belirlemekk istersek (s�n�f i�erisindeki optionBuilderdan belirlemektense bu tercih edilir) burada belirlemelisiniz

            //Pool kullanmak bir singletonPattern g�revi g�r�r
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //Ba�lant� ayar�m�z� burada belirlemi�� olduk. Context s�n�f�nda ayar�n� yapmam�z laz�m 
                      
            //Yukar�daki ifadede dikkat ederseniz UseLazyLoadingProxies ifadesi kullan�lm�st�r. Bu durum .NetCore'daki Lazy Loading'in s�rekli tetiklenebilmesi ad�na environment'inizi garanti alt�na alman�z� saglar.


            //***�nemli: Authentication i�lemini yapabilmek i�in servisi burada yaratmak gerekir.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.LoginPath = "/Home/Login";

            });


            //session kullanacak iseniz ayarlamalar�n� yapmay� sak�n unutmay�n
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20); //Al��veri� bo� durdu�unda ne kadar dursun zamanlamas�
                x.Cookie.HttpOnly = true; //Protokol g�venli�i
                x.Cookie.IsEssential = true; //Bu da g�venlikle ilgili
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

            //Authentication� Authorizationdan �nce vermeye �zen g�stermek gerekir.
            app.UseAuthentication();//kullan�c� kim bunu alg�lar
            
            app.UseAuthorization(); //Yetkimiz var m� yok mu yani durumlar�nda (Authorization) �al��acak metotdur.

            //Session'� ekledikten sonra kullanmay� unutmayaca��z.
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
