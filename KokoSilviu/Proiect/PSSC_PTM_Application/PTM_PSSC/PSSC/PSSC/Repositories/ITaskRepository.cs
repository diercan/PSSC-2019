using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace PSSC.Repositories
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task Create(PSSC.Models.Task task);
        System.Collections.IList GetAllTasks();
        System.Threading.Tasks.Task Delete(string taskID);
        PSSC.Models.Task GetTask(int taskID);
        System.Threading.Tasks.Task UpdateTaskStatus(PSSC.Models.Task task);
        System.Threading.Tasks.Task UpdateTask(PSSC.Models.Task task);
        int GetPlannedNr(string uid);
        int GetInWorkN(string uid);
        int GetRealizedNr(string uid);
        int GetCanceledNr(string uid);
    }
}

