using System;
using System.ComponentModel.DataAnnotations;

namespace scoala.Models
{
    public class Situatie
    {
        public IDElev IDElev { get; set; }
        public IDSituatie IDSituatie { get; set; }
        public DateTime Data { get; set; }
        public Materie Materie { get; set; }
        public Nota Nota { get; set; }
        public SituatieOra SituatieOra { get; set; }

        public virtual Elev Elev { get; set; }
    }
}