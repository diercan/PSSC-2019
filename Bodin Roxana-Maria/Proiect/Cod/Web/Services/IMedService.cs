using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models;

namespace Web.Services
{
    public interface IMedService
    {
         Task Initialize();

         Task<List<MedEntity>> GetMeds();
         Task<List<MedEntity>> GetMedByUserName(string username);        
         Task<TableResult> AddMed(MedEntity med);
    }
}
