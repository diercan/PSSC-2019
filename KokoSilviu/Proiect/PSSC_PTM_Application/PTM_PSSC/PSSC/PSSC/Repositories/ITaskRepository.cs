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
        System.Threading.Tasks.Task Delete(PSSC.Models.Task task);
        PSSC.Models.Task GetTask(int taskID);
        System.Threading.Tasks.Task UpdateTask(PSSC.Models.Task task);

    }
}

