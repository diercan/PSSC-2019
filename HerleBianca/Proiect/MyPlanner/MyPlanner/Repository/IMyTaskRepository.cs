using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Repository
{
    public interface IMyTaskRepository
    {
        public void UpdateTask(string option,Models.DDD.MyTask myTask,string updated);
        public void AddTask(Models.DDD.MyTask myTask);
        public Models.DDD.MyTask FindTaskById(string Id);
        public List<Models.DDD.MyTask> GetAllTasks();
    }
}
