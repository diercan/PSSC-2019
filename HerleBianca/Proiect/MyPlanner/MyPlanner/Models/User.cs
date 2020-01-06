using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Models
{
    public class User
    {
        public Guid id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Username")]
        public string username { get; set; }
        [Display(Name = "Password")]
        public string encrypted_password { get; set; }
        [Display(Name = "Age")]
        public int age { get; set; }
        [Display(Name = "Other Ocupation")]
        public string other_ocupation { get; set; }
        [Display(Name = "Working at")]
        public List<MyTask> MyTasks_asigned { get; set; }
        [Display(Name = "In charge of")]
        public List<MyTask> MyTasks_owner { get; set; }
        public User()
        {
            this.id = new Guid();
            this.name = "None";
            this.username = "None";
            this.encrypted_password = "None";
            this.age = 0;
            this.other_ocupation = "None";
            this.MyTasks_asigned = null;
            this.MyTasks_owner = null;
        }
        public User(string name, string username, string encrypted_password)
        {
            this.id = new Guid();
            this.name = name;
            this.username = username;
            this.encrypted_password = encrypted_password;
            this.age = 0;
            this.other_ocupation = "None";
            this.MyTasks_asigned = null;
            this.MyTasks_owner = null;
        }
    }
}
