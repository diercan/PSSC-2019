using Proiect_PSSC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.ViewModels
{
    public class CreatePacientiViewModel
    {
      
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public long CNP { get; set; }
        public Sex Sexul { get; set; }

        public string Adresa { get; set; }
    }
}
