using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FrBaschet.Domain.Entities;
using FrBaschet.Domain.Interfaces;
using FrBaschet.Infrastructure.Data;
using FrBaschet.Infrastructure.Data.Context;
using FrBaschet.Infrastructure.Data.Repository;
using FrBaschet.Services.Interfaces;
using FrBaschet.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace FrBaschet.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<FrBaschetContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<FrBaschetContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                // options.Password.RequireDigit = true;
                // options.Password.RequireLowercase = true;
                // options.Password.RequireNonAlphanumeric = true;
                // options.Password.RequireUppercase = true;
                // options.Password.RequiredLength = 6;
                // options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FrBaschetContext>();
                context.Database.Migrate();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                foreach (var role in RoleHelper.GetRoles())
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                        Console.WriteLine($"rol create pentru {role}");
                    }
                }
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
#if DEBUG
            try
            {
                File.WriteAllText(
                    Path.Combine("..", "browsersync-update.txt"),
                    DateTime.Now.ToString()
                );
            }
            catch (Exception e)
            {
                var errorWriter = Console.Error;
                errorWriter.WriteLine(e.Message);
                errorWriter.WriteLine(e.StackTrace);
            }
#endif
        }
    }
}