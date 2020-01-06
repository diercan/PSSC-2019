using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPlanner.Repository;
using MyPlanner.Models.DDD;

namespace MyPlanner.Services
{
    public class CommandLine
    {
       public  MyTaskRepository repository;
        public static void Run()
        {
            var repository = new Repository.MyTaskRepository();
            string command;
            do
            {
                Console.WriteLine("Insert option: DisplayAll, AddTask, UpdateTask, GenerateReport, Exit");
                command = Console.ReadLine();
                switch (command)
                {
                    case "AddTask":
                        Console.WriteLine("Provide description due_date project owner asignee review");
                        string read = Console.ReadLine();
                        var function_params = read.Split(" ");
                        var new_task = MyTaskFactory.Instance.CreateTask(function_params[0], DateTime.Parse(function_params[1]), function_params[2], function_params[3], function_params[4], StatusType.InProgress, function_params[5]);
                        repository.AddTask(new_task);
                        break;
                    case "DisplayAll":
                        Console.WriteLine(repository.DisplayAll());
                        break;
                  
                    case "GenerateReport":
                        Console.WriteLine("Provide project name");
                        string project = Console.ReadLine();
                        var service = new GenerateReportService();
                        service.ActivityPerProject(repository, project);
                        break;
                    case "UpdateTask" :
                        Console.WriteLine("Provide option AssignTask, ChangeStatus, GiveReview");
                        string option = Console.ReadLine();
                        Console.WriteLine("Provide ID");
                        string read2 = Console.ReadLine();
                        Console.WriteLine("Provide field for changing");
                        string read3 = Console.ReadLine();
                        var updated = repository.FindTaskById(read2);
                        repository.UpdateTask(option, updated, read3);
                        break;
                }
               
            } while (command != "Exit");
        }
    }
}
