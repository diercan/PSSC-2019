using Farmacie.Models.CommonComponents;
using Farmacie.Models.DistribuitorComponents;
using Farmacie.Models.FarmacistComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models
{
    public class Distribuitor
    {
        private NumeDistribuitor numeDistribuitor;
        private EmailD emailDistribuitor;
        private NrTelefonD nrTelefonD;
        private AdresaDistribuitor adresaDistribuitor;
        
        public Distribuitor()
        {

        }
        public void CreateValueObject(Distribuitor d)
        {
            this.numeDistribuitor= new NumeDistribuitor(new Text(d.strNumeDistribuitor));
            this.emailDistribuitor = new EmailD(new Text(d.strEmailDistribuitor));
            this.nrTelefonD = new NrTelefonD(new Text(d.strNrTelefonDistribuitor));
            this.adresaDistribuitor = new AdresaDistribuitor(new Text(d.strAdresaDistribuitor));
        }

        public void CreateDBObject()
        {
           
            this.strNumeDistribuitor = numeDistribuitor.getNumeDistribuitor.getString;
            this.strEmailDistribuitor = emailDistribuitor.getEmail.getString;
            this.strNrTelefonDistribuitor = nrTelefonD.getNrTelefonD.getString;
            this.strAdresaDistribuitor = adresaDistribuitor.getAdresa.getString;

        }
        [Key]
        public int ID { get; set; }

        [Column("NumeDistribuitor")]
        [Required(ErrorMessage = "Introduceti numele")]
        public String strNumeDistribuitor { get; set; }

        [Column("EmailDistribuitor")]
        [Required(ErrorMessage = "Introduceti email-ul")]
        public String strEmailDistribuitor { get; set; }
        [Column("NrTelefonDistribuitor")]
        [Required(ErrorMessage = "Introduceti numarul de telefon")]
        public String strNrTelefonDistribuitor { get; set; }

        [Column("AdresaDistribuitor")]
        [Required(ErrorMessage = "Introduceti adresa distribuitorului")]
        public String strAdresaDistribuitor{ get; set; }
    }
}
