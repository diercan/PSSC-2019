using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageBroker _broker;
        private List<Task> tasks;

        public Worker(ILogger<Worker> logger,IMessageBroker broker)
        {
            _logger = logger;
            _broker = broker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Task.Run(() => _broker.Receive("RentToWorker"));
                Task.Run(() => _broker.Receive("ReturnToWorker"));
                Thread.Sleep(10000);
            }
        }


    }
}
