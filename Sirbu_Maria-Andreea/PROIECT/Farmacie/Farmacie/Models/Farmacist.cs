using Farmacie.Models.CommonComponents;
using Farmacie.Models.FarmacistComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models
{
    public class Farmacist
    {
        // Nume, Prenume, CNP, E-mail, NrTelefon, Adresa Farmacie
        private Text Nume, Prenume;
        private CNP cnp;
        private NrTelefon nrTelefon;
        private Email email;
        private Parola parola;
        private AdresaFarmacie adresaFarmacie;
      
        public Farmacist()
        {

        }

        public void CreateValueObject(Farmacist f)
        {
            this.Nume = new Text(f.strNume);
            this.Prenume = new Text(f.strPrenume);
            this.cnp = new CNP(new Text(f.strCNP));
            this.nrTelefon = new NrTelefon(new Text(f.strNrTelefon));
            this.email= new Email(new Text(f.strEmail));
            this.adresaFarmacie = new AdresaFarmacie(new Text(f.strAdresaFarmacie));
            this.parola = new Parola(new Text(f.strParola));
        }

        public void CreateDBObject()
        {
            this.strNume = Nume.getString;
            this.strPrenume = Prenume.getString;
            this.strCNP = cnp.getCNP.getString;
            this.strNrTelefon = nrTelefon.getNrTelefon.getString;
            this.strEmail = email.getEmail.getString;
            this.strAdresaFarmacie = adresaFarmacie.getAdresa.getString;
            this.strParola = parola.getParola.getString;

        }

        [Key]
        public int ID { get; set; }

        [Column("Nume")]
        [Required(ErrorMessage = "Introduceti numele")]
        public String strNume { get; set; }

        [Column("Prenume")]
        [Required(ErrorMessage = "Introduceti prenumele")]
        public String strPrenume { get; set; }

        [Column("CNP")]
        [Required(ErrorMessage = "Introduceti cnp-ul")]
        public String strCNP { get; set; }

        [Column("NrTelefon")]
        [Required(ErrorMessage = "Introduceti numarul de telefon")]
        public String strNrTelefon { get; set; }

        [Column("Email")]
        [Required(ErrorMessage = "Introduceti email-ul")]
        public String strEmail { get; set; }


        [Column("AdresaFarmacie")]
        [Required(ErrorMessage = "Introduceti adresa farmaciei")]
        public String strAdresaFarmacie { get; set; }

        [Column("Parola")]
        [Required(ErrorMessage = "Introduceti parola formata din 6 cifre")]
        public String strParola { get; set; }

    }
}
