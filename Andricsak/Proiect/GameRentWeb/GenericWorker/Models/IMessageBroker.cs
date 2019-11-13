using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericWorker
{
    public interface IMessageBroker : IDisposable
    {
        public  Task Receive();
        public  Task SendMessage(string message);
        public new void Dispose();
    }
}
