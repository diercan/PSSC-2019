using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_CookBook_PSSC.Controllers;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Repositories;
using MVC_CookBook_PSSC.Services;
using RabbitMQ.Client;

namespace MVC_CookBook_PSSC
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
            services.AddControllersWithViews();

            services.AddDbContext<UserDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultSQL")));
            //services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultSQLServer")));
            services.AddTransient<IUserRepository, UserRepository>();
            
            services.AddDbContext<RecipeDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultSQL")));
            services.AddTransient<IRecipeRepository, RecipeRepository>();

            services.AddTransient<MessageBroker, MessageBroker>();
            services.AddSingleton(new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("AmqpConnectionString")) }.CreateConnection());



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IRecipeRepository, RecipeRepository>();
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
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });

           
           
        }
    }
}
