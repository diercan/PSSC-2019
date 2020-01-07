using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPlanner.Models.DDD;
using MyPlanner.Repository;

namespace MyPlanner.Services
{
    public class GenerateReportService
    {
        public void ActivityPerProject(IMyTaskRepository repository,string project)
        {
            List<Models.DDD.MyTask> tasks = repository.GetAllTasks();
            foreach(Models.DDD.MyTask t in tasks)
            {
                if (t.Project.Equals(project)) {
                    foreach (string ev in t.Logger.GetSessionEvents())
                    {
                        Console.WriteLine(ev);
                    }
                }
                
            }
        }
    }
}

