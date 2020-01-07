using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPlanner.Models;

namespace MyPlanner.Repository
{
    public class MyTaskRepository : IMyTaskRepository
    {
        private static List<Models.DDD.MyTask> _tasks = new List<Models.DDD.MyTask>();

        public MyTaskRepository()
        {
        }

        public void AddTask(Models.DDD.MyTask task)
        {
            var result = _tasks.FirstOrDefault(d => d.Equals(task));

            if (result != null) throw new DuplicateWaitObjectException();

            _tasks.Add(task);
          
        }

        public void UpdateTask(string option,Models.DDD.MyTask task,string updated)
        {
           foreach(Models.DDD.MyTask t in _tasks)
            {
                if(t.Id==task.Id)
                {
                    switch (option)
                    {
                        case "AssignTask":
                            t.AssignTask(new Models.DDD.Volunteer(updated));
                            break;
                        case "ChangeStatus":
                            t.ChangeStatus(Models.DDD.MyTask.Convert(updated));
                            break;
                        case "GiveReview":
                            t.GiveReview(new Models.DDD.PlainText(updated));
                            break;
                    }

                }
            }
            
        }

        public Models.DDD.MyTask FindTaskById(string Id)
        {
            return _tasks.FirstOrDefault(d => d.Id.ToString() == Id);
        }

        public List<Models.DDD.MyTask> GetAllTasks()
        {
            return _tasks;
        }

        public string DisplayAll()
        {
            foreach (Models.DDD.MyTask t in _tasks)
            {
                return t.ToString();
            }
            return string.Empty;
        }
    }
}
