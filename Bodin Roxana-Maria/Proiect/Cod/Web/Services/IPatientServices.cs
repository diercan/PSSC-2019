using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models;

namespace Web.Services
{
    public interface IPatientService
    {
        Task Initialize();
       
        Task<List<PatientEntity>> GetPatients();
        Task<List<PatientEntity>> GetPatientByUserName(string username);
        
        Task<TableResult> AddPatient(PatientEntity patient);
    }
}
