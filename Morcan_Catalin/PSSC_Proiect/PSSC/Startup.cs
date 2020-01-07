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
using PSSC.Repository;
using PSSC.Services;
using RabbitMQ.Client;
using MassTransit.RabbitMqTransport;

namespace PSSC
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
                var host = sbc.Host(new Uri("amqp://vkkjzmlp:JgF_kO2IPRHpu4mdmLavflHwkvr9Lws-@dove.rmq.cloudamqp.com/vkkjzmlp"), h =>
                {
                    h.Username("vkkjzmlp");
                    h.Password("JgF_kO2IPRHpu4mdmLavflHwkvr9Lws-");
                });
            });

            bus.Start();

            var repository = new PostareRepository();
            var userRepository = new UserRepository();

            var userService = new LoginService(userRepository);
            var service = new PostareService(bus ,repository);

            services.AddSingleton<IPostareRepository>(repository);
            services.AddSingleton<IPostareService>(service);

            services.AddSingleton<IUserRepository>(userRepository);
            services.AddSingleton<ILoginService>(userService);

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
						 pattern: "{controller=Login}/{action=Login}/{id?}");
			});
		}
	}
}
