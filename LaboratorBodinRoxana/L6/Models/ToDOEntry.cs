using System;
namespace lab6
{
    public class ToDOEntry
    {
        public string titlu{ get;}
        public string mesaj{get;}
        public DateTime DateTime{get;}
        public ToDOEntry(string t, string m , DateTime time)
        {
            this.titlu=t;
            this.mesaj=m;
            this.DateTime=time;
        }


    }
}