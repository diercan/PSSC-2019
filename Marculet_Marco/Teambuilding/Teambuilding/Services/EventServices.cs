using MassTransit;
using Teambuilding.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teambuilding.Models;
using Teambuilding.Repository;
using MessageConsole;

namespace Teambuilding.Services
{
    public interface IEventServices
    {
        void createEvent(Event events);
        List<Event> getAllEvents();
        Event getEventById(Guid id);
        void deleteEvent(Guid id);
    }

    public class EventServices : IEventServices
    {
        private readonly IBusControl bus;
        private readonly IEventRepository eventRepository;

        public EventServices(IBusControl bus, IEventRepository eventRepository)
        {
            this.bus = bus;
            this.eventRepository = eventRepository;
        }

        public void createEvent(Event events)
        {

            eventRepository.createEvent(events);

            bus.Publish(new EventNotification()
            {
                id = events.id,
                date = events.date,
                text = events.text
            }); 
        }

        public void deleteEvent(Guid id)
        {
            Event events = eventRepository.getEventById(id);
            eventRepository.deleteEvent(events);

            bus.Publish(new Event()
            {
                id = events.id,
                date = events.date,
                text = events.text
            });
        }

        public List<Event> getAllEvents()
        {
            return eventRepository.getAllEvents();
        }

        public Event getEventById(Guid id)
        {
            return eventRepository.getEventById(id);
        }
    }
}
