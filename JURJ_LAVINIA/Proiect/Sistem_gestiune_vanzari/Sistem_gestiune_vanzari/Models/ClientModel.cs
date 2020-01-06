using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models
{
    public class ClientModel
    {
        public int identitate_client { get; set; }
        [Required(ErrorMessage = "Introduceti numele clientului")]
        [DisplayName("Nume Client")]
        public string nume_client { get; set; }
        [Required(ErrorMessage = "Introduceti prenumele clientului")]
        [DisplayName("Prenume Client")]
        public string prenume_client { get; set; }
        [Required(ErrorMessage = "Introduceti adresa clientului")]
        [DisplayName("Adresa")]
        public string adresa_client { get; set; }
        [Required(ErrorMessage = "Introduceti mailul clientului")]
        [DisplayName("Mail")]
        public string email_client { get; set; }
        public string parola_client { get; set; }
    }
}