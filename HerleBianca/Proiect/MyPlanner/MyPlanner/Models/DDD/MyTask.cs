using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Models.DDD
{
   
    public class MyTask
    {
        public Guid Id { get; internal set; }
        public PlainText Description { get; internal set; }
        public CalendarDate Due_Date { get; internal set; }
        public PlainText Project { get; internal set; }
        public Volunteer Owner { get; internal set; }
        public Volunteer Asignee { get; internal set; }
        public PlainText Review { get; internal set; }
        public StatusType Status { get; internal set; }
        public IEventLogger Logger { get; internal set; }

        internal MyTask(PlainText description, CalendarDate due_date, PlainText project, Volunteer owner, Volunteer asignee, PlainText review, StatusType status)
        {
            Id = new Guid();
            Description = description;
            Due_Date = due_date;
            Project = project;
            Owner = owner;
            Asignee = asignee;
            Review = review;
            Status = status;
            Logger = new EventLogger();
        }

        internal MyTask(PlainText description, CalendarDate due_date, PlainText project, Volunteer owner, Volunteer asignee, PlainText review, StatusType status, IEventLogger Logger)
        {
            Id = new Guid();
            Description = description;
            Due_Date = due_date;
            Project = project;
            Owner = owner;
            Asignee = asignee;
            Review = review;
            Status = status;
            this.Logger = Logger;
        }

        #region operatii
        public void AssignTask(Volunteer volunteer)
        {
            Asignee = volunteer;
            Logger.Log("Project : "+ Project.ToString()+ " Task : " + Description.ToString() + " was assigned to : " + Asignee.ToString());
        }

        public void ChangeStatus(StatusType status)
        {
            Status = status;
            Logger.Log("Project : " + Project.ToString() + " Task : " + Description.ToString() + " status has changed to : " + Status.ToString());
        }

        public void GiveReview(PlainText review)
        {
            Review = review;
            Logger.Log("Project : " + Project.ToString() + " Task : " + Description.ToString() + " has received a review : " + Review.ToString());
        }

        public override string ToString()
        {
            string format =
                "=============================================== \n" +
                "[Id] " + Id.ToString() +
                "\n[Description] " + Description.ToString() +
                "\n[Due_date] " + Due_Date.ToString() +
                "\n[Project] " + Project.ToString() +
                "\n[Owner] " + Owner.ToString() +
                "\n[Asignee] " + Asignee.ToString() +
                "\n[Status] " + Status.ToString() +
                "\n[Review] " + Review.ToString() +
                "\n=============================================== \n";
            return format;
        }
        public static StatusType Convert(string word)
        {
            switch (word)
            {
                case "NotStarted":
                    return StatusType.NotStarted;
                case "InProgress":
                    return StatusType.InProgress;
                case "Blocked":
                    return StatusType.Blocked;
                case "Done":
                    return StatusType.Done;
            }
            return StatusType.NotStarted;

        }
        #endregion
    }
}
