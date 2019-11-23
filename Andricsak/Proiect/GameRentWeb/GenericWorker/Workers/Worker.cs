using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GenericWorker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Workers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageBroker _broker;
        public Worker(ILogger<Worker> logger,IMessageBroker broker)
        {
            _logger = logger;
            _broker = broker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {               
                Task.Run(() => _broker.Receive("RentToWorker"));
                Task.Run(() => _broker.Receive("ReturnToWorker"));
                Thread.Sleep(5000);
            }
        }


    }
}
