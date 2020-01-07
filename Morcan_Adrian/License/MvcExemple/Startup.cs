using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LicenseWeb.Repository;
using MassTransit;
using LicenseWeb.Services;

namespace LicenseWeb
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

			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				var host = sbc.Host(new Uri("amqp://msmcpkzk:nlP6hJVvCr3mgyr8EI-XU1or5QatPXGe@dove.rmq.cloudamqp.com/msmcpkzk"), h =>
				{
					h.Username("msmcpkzk");
					h.Password("nlP6hJVvCr3mgyr8EI-XU1or5QatPXGe");
				});
			});

			bus.Start();

			var repository = new LicenseRepository();
			var service = new LicenseService(bus, repository);
			
			services.AddSingleton<ILicensesRepository>(repository);
			services.AddSingleton<ILicenseService>(service);

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
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
						 name: "default",
						 pattern: "{controller=Licenses}/{action=Index}/{id?}");
			});
		}
	}
}
