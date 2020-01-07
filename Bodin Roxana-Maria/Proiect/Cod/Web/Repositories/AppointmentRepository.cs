using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.DDD;
using Web.Models;
using Web.Services;

namespace Web.Repositories{
    public class AppointmentRepository : IAppointmentRepository
    {
        
        private readonly IAppointmentService _appointmentService;
        public AppointmentRepository(IAppointmentService appointmentService)
        {
            _appointmentService=appointmentService;
        }
        public async void CreateAppointment(Appointment appointment){
            await _appointmentService.Initialize();
            List<Appointment> id= await _appointmentService.GetAppointment();
            appointment.Id=""+id.Count;
            await _appointmentService.AddAppointment(appointment);
        }
    
        public async Task<List<Appointment>> GetAppointment(string id){
            return await _appointmentService.GetAppointmentById(id);
        }
        public async Task<int> GetAllAppointments(){
            List<Appointment> all=await _appointmentService.GetAppointment();
            return all.Count;
        }
        public async void UpdateAppointment(Appointment appointment){
             await _appointmentService.UpdateAppointment(appointment);
        }
        public async void DeleteAppointment(string id){
            await _appointmentService.Initialize();
            List<Appointment> Appointment= await _appointmentService.GetAppointmentById(id);
           _appointmentService.Delete(Appointment[0]);
        }
    }
}