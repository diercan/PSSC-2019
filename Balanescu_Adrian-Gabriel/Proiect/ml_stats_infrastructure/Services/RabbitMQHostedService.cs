using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ml_stats_core.Interfaces;
using ml_stats_core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ml_stats_infrastructure.Services
{
  public class RabbitMQHostedService : BackgroundService
  {
    private readonly ILogger _logger;  
    private IConnection _connection;  
    private IModel _channel;
    private readonly IPlotPointItemRepository _plotPointRepo;
    private readonly IHubContext<ExpDataHub> _expHubContext;
    
    public RabbitMQHostedService(ILoggerFactory loggerFactory, IPlotPointItemRepository plotPointRepo, IHubContext<ExpDataHub> hubContext)
    {
      _logger = loggerFactory.CreateLogger<RabbitMQHostedService>();
      _plotPointRepo = plotPointRepo;
      _expHubContext = hubContext;
      InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
      var factory = new ConnectionFactory(){HostName = "localhost"};

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
      _channel.ExchangeDeclare("experiments", ExchangeType.Direct);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      stoppingToken.ThrowIfCancellationRequested();
      var queueName = _channel.QueueDeclare().QueueName;
      
      _channel.QueueBind(queue: queueName, exchange: "experiments", routingKey: "exps_stream");
      
      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += async (model, ea) =>
      {
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        var point = JsonConvert.DeserializeObject<PlotPoint>(message);
        point.TimeStamp = DateTime.UtcNow;
        await _expHubContext.Clients.All.SendAsync("onExpData", point);
        var addedPoint = await _plotPointRepo.AddAsync(point);
        _logger.LogInformation($"{routingKey} : {message}");
      };
      
      _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
      return Task.CompletedTask;
    }

   /* private async Task<IEnumerable<Experiment>> getExperiments()
    {
      try
      {
        string sql = $"SELECT * FROM s";
        var exp = await _experimentItemRepository.GetByExpressionAsync(sql, new SqlParameterCollection());
        return exp;
      }
      catch(Exception e)
      {
        throw;
      }
    }
    
    private async Task<Experiment> updateExperiment(string expId, Experiment updatedItem)
    {
      if (updatedItem.Id.Split(":")[1] != expId)
      {
         return updatedItem;
      }
      try
      {
        updatedItem.Id = updatedItem.Id.Split(":")[1];
        return await _experimentItemRepository.UpdateAsync(updatedItem);
      }
      catch(Exception e)
      {
        throw;
      }
    }
    
    private async Task<Experiment> getExperiment(string expId)
    {
      try
      {
       return await _experimentItemRepository.GetByIdAsync(expId);
      }
      catch(Exception e)
      {
        throw;
      }
    }*/
    
    public override void Dispose()  
    {  
      _channel.Close();
      _connection.Close();  
      base.Dispose();  
    }  
  }
}