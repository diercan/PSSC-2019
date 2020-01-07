using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Programare
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public WashType Washtype { get; set; }

        [Required]
        public string Name { get; set; }

     

    }
    public enum WashType
    {
        Masina,
        Canapea,
        Saltea,
        Covor,
        Coltar
    }
}
