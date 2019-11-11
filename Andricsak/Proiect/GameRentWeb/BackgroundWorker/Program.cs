using GameRentWeb.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    class Program
    {
        static public async Task Init()
        {
            Console.WriteLine("Background worker started!");
            using (MessageBroker broker = new MessageBroker())
            {
                await broker.Receive("RentToWorker","WorkerToRent");
            }
        }
        static async Task Main(string[] args)
        {
            await Init();
        }
    }
}
