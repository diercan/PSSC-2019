using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericWorker
{
    public interface IMessageBroker
    {
        public  Task Receive(string queueReceive);
        public  Task SendMessage(string message,string queueSend);
    }
}
