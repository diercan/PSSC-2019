using Teambuilding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teambuilding.Repository
{
    public interface IEventRepository
    {

        void createEvent(Event events);

        List<Event> getAllEvents();

        Event getEventById(Guid id);

        void deleteEvent(Event events);

    }

    public class EventRepository : IEventRepository
    {
        private readonly List<Event> eventList;

        public EventRepository()
        {
            eventList = new List<Event>();

            eventList.Add(new Event
            {
                id = Guid.NewGuid(),
                date = DateTime.Now,
                text = "Predarea Proiectului",
            });

            eventList.Add(new Event
            {
                id = Guid.NewGuid(),
                date = new DateTime(2020,1,10),
                text = "Final semestru",
            });

            eventList.Add(new Event
            {
                id = Guid.NewGuid(),
                date = new DateTime(2021, 4, 12),
                text = "Vacanta",
            });

            eventList.Add(new Event
            {
                id = Guid.NewGuid(),
                date = new DateTime(2020, 12, 31),
                text = "Revelion",
            });

        }

        public void createEvent(Event events)
        {
            eventList.Add(events);
        }

        public void deleteEvent(Event events)
        {
            eventList.Remove(events);
        }

        public List<Event> getAllEvents()
        {
            return eventList;
        }

        public Event getEventById(Guid id)
        {
            return eventList.FirstOrDefault(_ => _.id == id);
        }
    }
}
