using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using StudentMVC.Models;

namespace StudentMVC.Services
{
    public class Service : IService
    {
        private readonly IBus bus;

        public Service(IBus bus)
        {
            this.bus = bus;
        }

        public void SendFeedback(Feedback feedback)
        {
            //List.Add(feedback);
            bus.Publish<Feedback>(feedback);
        }
    }
}
