using System;
using System.Collections.Generic;
using System.Linq;
using MVC_project.Models;

namespace MVC_project.Repository
{
    public interface IAppointmentsRepository 
    {
        void CreateAppointments(Appointments appointments);
        List<Appointments> GetAllAppointments();
        Appointments GetAppointmentsById(Guid id);
        void DeleteAppointments(Appointments appointments);
        void EditAppointments(Appointments appointments);
    }

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly List<Appointments> List;

        public AppointmentsRepository()
        {
            List = new List<Appointments>();
            List.Add(new Appointments
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Popa Ilona",
                Phone = 0721324567,
                TestType = TestType.CompleteBloodCount
            });
            List.Add(new Appointments
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Ungureanu Andrei",
                Phone = 0787389508,
                TestType = TestType.BasicMetabolicPanel
            });
            List.Add(new Appointments
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Acea Tiberiu",
                Phone = 0773351279, 
                TestType = TestType.ThyroidFunctionTests
            });
        }

        public void CreateAppointments(Appointments appointments)
        {
            List.Add(appointments);
        }

        public void DeleteAppointments(Appointments appointments)
        {
            List.Remove(appointments);
        }


        public void EditAppointments(Appointments appointments)
        {
            var app = List.FirstOrDefault(a => a.Id == appointments.Id);
            app.Date = appointments.Date;
            app.Name = appointments.Name;
            app.TestType = appointments.TestType;
            app.Phone = appointments.Phone;
        }

        public List<Appointments> GetAllAppointments()
        {
            return List;
        }

        public Appointments GetAppointmentsById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);
        }
    }
}
