using System;
using RabbitMQ.Client;

namespace ConsoleApp1
{
    public class Receiver : IDisposable
    {
        private IConnection _conn;
        private IModel _channel;

        public IModel channel => _channel;

        public Receiver()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://xqudjssl:st7qrJNH2_5_fE6HlNuWKzRXEzefN60A@dove.rmq.cloudamqp.com/xqudjssl");
            factory.UserName = "xqudjssl";
            factory.Password = "st7qrJNH2_5_fE6HlNuWKzRXEzefN60A";
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();
        }

        public void Dispose()
        {
            _channel.Close();
            _conn.Close();
        }
    }
}