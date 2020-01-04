using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace PSSC.Repositories
{
    public interface ITaskRepository
    {
        void Create(PSSC.Models.Task task);
        System.Collections.IList GetAllTasks();
        void Delete(string taskID);
        PSSC.Models.Task GetTask(int taskID);
        void UpdateTaskStatus(PSSC.Models.Task task);
        void UpdateTask(PSSC.Models.Task task);
        void AddDeveloper(PSSC.Models.Developer d);
        void DeleteDeveloper(PSSC.Models.Developer d);
        int GetPlannedNr(string uid,bool pm_user);
        int GetInWorkN(string uid, bool pm_user);
        int GetRealizedNr(string uid, bool pm_user);
        int GetCanceledNr(string uid, bool pm_user);
    }
}

