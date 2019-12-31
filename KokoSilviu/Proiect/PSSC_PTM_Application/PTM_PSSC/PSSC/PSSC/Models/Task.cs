using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public class Task
    {
        public int id { get; private  set; }
        public string name { get; private set; }
        public Developer author { get; private set; }
        public Developer developer { get; private set; }
        public string description { get; private set; }
        public string status { get; private set; }
        public string priority { get; private set; }
       
        public Task()
        {

        }

        public Task(int id,string name,Developer a, Developer d, string description,string status, string priority)
        {
            this.id = id;
            this.name = name;
            this.author = a;
            this.description = description;
            this.developer = d;
            this.status = status;
            this.priority = priority;
        }
        public void Create(Task t)
        {
            this.id = t.id;
            this.name = t.name;
            this.author = t.author;
            this.description = t.description;
            this.developer = t.developer;
            this.status = t.status;
            this.priority = t.priority;
        }
        public void ChangeID(int id)
        {
            this.id = id;
        }

        public void ChangeName(string s)
        {
            this.name = s;
        }

        public void ChangeDescription(string s)
        {
            this.description = s;
        }

        public void ChangeStatus(string s)
        {
            this.status = s;
        }
        
        public void ChangePrio(string s)
        {
            this.priority = s;
        }

        public void Assign(Developer d)
        {
            this.developer = d;
        }

        public void ChangeAuthor(Developer aut)
        {
            this.developer = aut;
        }

    }
}
