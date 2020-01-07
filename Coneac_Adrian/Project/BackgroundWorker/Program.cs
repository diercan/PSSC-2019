using BackgroundWorker.Models;
using System;

namespace BackgroundWorker
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            MessageBroker _broker = new MessageBroker();
            Console.WriteLine("Background worker started...");
            await _broker.Receive("WebToWorker", "WorkerToWeb");
        }
    }
}
