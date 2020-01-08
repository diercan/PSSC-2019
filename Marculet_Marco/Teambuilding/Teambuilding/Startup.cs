using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Teambuilding.Repository;
using Teambuilding.Services;

namespace Teambuilding
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
				var host = sbc.Host(new Uri("amqp://hwtjwzhp:AgmGeD5PSuocVNMgot9OncI595wDL7Uv@dove.rmq.cloudamqp.com/hwtjwzhp"), h =>
				{
					h.Username("hwtjwzhp");
					h.Password("AgmGeD5PSuocVNMgot9OncI595wDL7Uv");
				});
			});

			bus.Start();

			var userRepository = new CredentialsRepository();
			var eventRepository = new EventRepository();
			var eventService = new EventServices(bus, eventRepository);
			
			services.AddSingleton<ICredentialsRepository>(userRepository);
			services.AddSingleton<IEventRepository>(eventRepository);
			services.AddSingleton<IEventServices>(eventService);
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
						 pattern: "{controller=User}/{action=Login}/{id?}");
			});
		}
	}
}
