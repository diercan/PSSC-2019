using System.Collections.Generic;

namespace lab6
{
    public class ToDOList
    {
        public List<ToDOEntry> entryList=new List<ToDOEntry>();
        public ToDOList(List<ToDOEntry> entry)
        {
            this.entryList=entry;
        }
    }
}