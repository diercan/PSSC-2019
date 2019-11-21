using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace GenericWorker
{
    public class Program
    {
  
        public IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext,config) =>
            {
                config.AddJsonFile("appsettings.json",optional:true);
                config.AddEnvironmentVariables();
            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    IConfiguration configuration = hostContext.Configuration;                  
                    services.AddSingleton<IMessageBroker, MessageBroker>();                    
                    services.AddSingleton(new ConnectionFactory() { Uri = new Uri(hostContext.Configuration.GetConnectionString("AMQPConnectionString")) }.CreateConnection());
                });
    }
}
