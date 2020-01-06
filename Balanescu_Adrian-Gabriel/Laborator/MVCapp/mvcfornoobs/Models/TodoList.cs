using System.Collections.Generic;

namespace mvcfornoobs.Models
{
    public class TodoList
    {
        public List<TodoEntry> listOfEntries { get; set; }  
        public TodoList()
        {
            listOfEntries = new List<TodoEntry>();
        }
    }
}