using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using PSSC.Entities;


namespace PSSC.Repositories
{
    public interface ITaskRepository
    {
        Task Create(TaskEntity task);
        Task<List<TaskEntity>> GetAllTasks();
        Task DeleteTask(TaskEntity task);
        Task<TaskEntity> GetTask(int taskID);
        Task UpdateTaskStatus(TaskEntity task);
        Task UpdateTask(TaskEntity task);
        Task AddDeveloper(UserLogInEntity dev);
        Task DeleteDeveloper(UserLogInEntity dev);
        Task<int> GetPlannedNr(UserLogInEntity dev, bool pm_user);
        Task <int> GetInWorkN(UserLogInEntity dev, bool pm_user);
        Task<int> GetRealizedNr(UserLogInEntity devd, bool pm_user);
        Task<int> GetCanceledNr(UserLogInEntity dev, bool pm_user);
    }
}

