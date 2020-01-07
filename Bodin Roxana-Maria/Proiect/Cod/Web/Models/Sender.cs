using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace Web.Models
{
    public class Sender
    {
        public Sender()
        {

        }
        public string Publish(DateTime date)
        {
            //Next we create a channel, which is where most of the API for getting things done resides.
            //To send, we must declare a queue for us to send to; then we can publish a message to the queue:
            var factory = new ConnectionFactory() {  Uri =new Uri( "amqp://niifhhdp:gdvQnylJbzwz7zRYe2YccrCS3cRNNXQ2@dove.rmq.cloudamqp.com/niifhhdp" )};
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                string message = "An appointment was cancelled and now date"+ date +" it is free!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                routingKey: "hello",
                                basicProperties: null,
                                body: body);
                
                return message;
            }
        } 
    }
}
