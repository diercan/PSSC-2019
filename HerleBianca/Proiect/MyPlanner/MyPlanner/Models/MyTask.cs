using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Models
{
    public class MyTask //used with Entity Framework
    {
        [Display(Name = "Id")]
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { this._id = value; }
        }

        [Required]
        [Display(Name = "Description")]
        private string _description;
        public string Description
        {
            get { return _description; }
            set { this._description = value; }
        }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        private DateTime _due_date;
        
        public DateTime Due_Date
        {
            get { return _due_date; }
            set { this._due_date = value; }
        }

        [Display(Name = "Project")]
        private string _project; //each task is part of a project
        public string Project
        {
            get { return this._project; }
            set { this._project = value; }
        }

        [Display(Name = "Owner")]
        private string _owner_name;
        public string Owner
        {
            get { return this._owner_name; }
            set { this._owner_name = value; }
        }

        [Display(Name = "Asignee")]
        private string _asignee_name;
        public string Asignee
        {
            get { return this._asignee_name; }
            set { this._asignee_name = value; }
        }

        private string _review { get; set; }
        public string Review
        {
            get { return this._review; }
            set { this._review = value; }
        }

        [Display(Name = "Status")]
        public StatusType _status;
        public StatusType Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        public enum StatusType
        {
            [Display(Name = "Not Started")]
            NotStarted,
            [Display(Name = "In Progress")]
            InProgress,
            Blocked,
            Done
        }

        public MyTask()
        {
            this._id = new Guid();
            this._description = "default description";
            this._due_date = DateTime.Today;
            this._project = "default project";
            this._owner_name = "default owner name";
            this._asignee_name = "default asignee name";
            this._status = StatusType.NotStarted;
            this._review = "None";
        }
        public MyTask(string description, DateTime due_date, string project, string _owner_name, string _asignee_name, StatusType status,string review)
        {
            this._id = new Guid();
            this._description = description;
            this._due_date = due_date;
            this._project = project;
            this._owner_name = _owner_name;
            this._asignee_name = _asignee_name;
            this._status = status;
            this._review = review;
        }

    }
}

