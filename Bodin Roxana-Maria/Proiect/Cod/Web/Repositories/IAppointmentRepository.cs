using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.DDD;
using Web.Models;
using Web.Services;


namespace Web.Repositories{
    public interface IAppointmentRepository{
        void CreateAppointment(Appointment appointment);
        Task<List<Appointment>> GetAppointment(string id);
        Task<int> GetAllAppointments();
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(string id);
    }
}