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
        public string CNP { get; set; }
        public Sex Sexul { get; set; }

        public string Adresa { get; set; }

        public string Inaltime { get; set; }

        public string Greutate { get; set; }

        public string Temperatura { get; set; }

        public string Puls { get; set; }

        public string Tensiune { get; set; }

        public string Frecventa_Cardiaca { get; set; }

        public string Frecv_Respiratorie { get; set; }

        public string Inspectie_Toracica { get; set; }

        public string Auscultatie { get; set; }

        public string Hemoglobina { get; set; }

        public string Leucocite { get; set; }

        public string Eritrocite { get; set; }

        public string Trombocite { get; set; }

        public string Hematocrit { get; set; }

    }
}
