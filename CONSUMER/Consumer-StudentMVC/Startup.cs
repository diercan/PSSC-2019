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
using StudentMVC.Models;
using StudentMVC.Repository;

namespace StudentMVC
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

            // trimite prin massTransit rabbitMQ   publish-subscribe 

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {

                //host config
                var host = cfg.Host(new Uri("amqp://nqfmkaew:nIrU1GP9_2-AppQNTD8jzvynPPqvHWIR@dove.rmq.cloudamqp.com/nqfmkaew"), h =>
                {
                    h.Username("nqfmkaew");
                    h.Password("nIrU1GP9_2-AppQNTD8jzvynPPqvHWIR");
                });

                //queque config
                cfg.ReceiveEndpoint(host, "FeedbackQueque1", e =>
                {
                    e.Consumer<MessageConsumer>();

                    //e.Handler<Feedback>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    //});
                }
                   );

            });












            //var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            //{
            //    var host = sbc.Host(new Uri("amqp://nqfmkaew:nIrU1GP9_2-AppQNTD8jzvynPPqvHWIR@dove.rmq.cloudamqp.com/nqfmkaew"), h =>
            //    {
            //        h.Username("nqfmkaew");
            //        h.Password("nIrU1GP9_2-AppQNTD8jzvynPPqvHWIR");
            //    });


            //    sbc.ReceiveEndpoint(e =>
            //    {
            //        e.Consumer<MessageConsumer>();
            //    });
            //});




            bus.Start();




            ///////adaug serviciul dedicat din repository
            var repo = new FeedbackRepo();   //bus

            services.AddSingleton<IFeedbackRepo>(repo);


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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
