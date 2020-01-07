using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models.DDD;

namespace Web.Services
{
    public interface IAppointmentService
    {
        Task Initialize();

        Task<List<Appointment>> GetAppointment();
        Task<List<Appointment>> GetAppointmentByMed(string medName);
        Task<List<Appointment>> GetAppointmentByPatient(string patientName);
        
        Task<List<Appointment>> GetAppointmentById(string id);
        Task<TableResult> AddAppointment(Appointment app);
        
        Task<TableResult> UpdateAppointment(Appointment app);
        void Delete(Appointment appointment);
    }
}
