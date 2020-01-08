using System;
using RabbitMQ.Client;

namespace FrBaschet.Services.Services
{
    public class Sender : IDisposable
    {
        private IConnection _conn;

        public IModel channel { get; private set; }

        public void Dispose()
        {
            Console.WriteLine("DISTRUSSSSS!!!");
            channel.Close();
            _conn.Close();
        }

        public void Connect()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://xqudjssl:st7qrJNH2_5_fE6HlNuWKzRXEzefN60A@dove.rmq.cloudamqp.com/xqudjssl");
            factory.UserName = "xqudjssl";
            factory.Password = "st7qrJNH2_5_fE6HlNuWKzRXEzefN60A";
            _conn = factory.CreateConnection();
            channel = _conn.CreateModel();
            Console.WriteLine("CONECTAT!!!!");
        }
    }
}