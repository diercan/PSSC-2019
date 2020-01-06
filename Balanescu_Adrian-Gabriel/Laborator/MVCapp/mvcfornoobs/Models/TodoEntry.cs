using System;

namespace mvcfornoobs.Models
{
    public class TodoEntry
    {
        public TodoEntry(string titlu, string mesaj, DateTime datetime)
        {
            this.Titlu = titlu;
            this.Mesaj = mesaj;
            this.Time = datetime;
        }
        public string Titlu { get; set; }
        public string Mesaj { get; set; }
        public DateTime Time { get; set; }
    }
}