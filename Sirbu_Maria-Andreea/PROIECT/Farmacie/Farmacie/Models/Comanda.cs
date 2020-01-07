using Farmacie.Models.ComandaComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models
{
    public class Comanda
    {
        private NumeFarmacist numeFarmacist;
        private PrenumeFarmacist prenumeFarmacist;
        private EmailFarmacist emailFarmacist;
        private EmailDistribuitor emailDistribuitor;
        private NumeMedicament numeMedicament;
        private Cantitate cantitate;
        private AdresaFarmacie adresa;
        public Comanda()
        {

        }

        [Key]
        public int ID { get; set; }

        [Column("NumeFarmacist")]
        [Required(ErrorMessage = "Introduceti numele")]
        public String strNumeFarmacist { get; set; }

        [Column("PrenumeFarmacist")]
        [Required(ErrorMessage = "Introduceti prenumele")]
        public String strPrenumeFarmacist { get; set; }

        [Column("EmailFarmacist")]
        [Required(ErrorMessage = "Introduceti email-ul")]
        public String strEmailFarmacist { get; set; }

        [Column("EmailDistribuitor")]
        [Required(ErrorMessage = "Introduceti email-ul")]
        public String strEmailDistribuitor { get; set; }

        [Column("NumeMedicament")]
        [Required(ErrorMessage = "Introduceti numele")]
        public String strNumeMedicament { get; set; }

        [Column("Cantitate")]
        [Required(ErrorMessage = "Introduceti cantitatea")]
        public int inCantitate { get; set; }

      

        [Column("AdresaFarmacie")]
        [Required(ErrorMessage = "Introduceti adresa farmaciei")]
        public String strAdresaFarmacie { get; set; }

    }
}
