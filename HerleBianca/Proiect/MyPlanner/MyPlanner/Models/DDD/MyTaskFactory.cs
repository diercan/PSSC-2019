using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Models.DDD
{
    public class MyTaskFactory
    {
        public static readonly MyTaskFactory Instance = new MyTaskFactory();
        private MyTaskFactory()
        {

        }
        public MyTask CreateTask(string description, DateTime due_date, string project, string owner_name, string asignee_name, StatusType status,string review)
        {
            
            var my_task = new MyTask(new PlainText(description), new CalendarDate(due_date),new PlainText (project), new Volunteer(owner_name), new Volunteer(asignee_name),new PlainText(review),status);
            return my_task;
        }
        public MyTask CreateTask(string description, DateTime due_date, string project, string owner_name, string asignee_name, StatusType status, string review, IEventLogger Logger)
        {

            var my_task = new MyTask(new PlainText(description), new CalendarDate(due_date), new PlainText(project), new Volunteer(owner_name), new Volunteer(asignee_name), new PlainText(review), status, Logger);
            return my_task;
        }
    }

    public class ArgumentInvalidLengthException : Exception
    {

    }
}
