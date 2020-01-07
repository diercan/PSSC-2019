using Farmacie.Models.CommonComponents;
using Farmacie.Models.MedicamentComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models
{
    public class Medicament
    {
        private NumeMedicament numeMedicament;
        private Stoc stoc;
        private DistribuitorM distribuitor;
        private PretVanzare pretVanzare;
        private PretCumparare pretCumparare;
        private Locatie adresaFarmacie;
      
        public Medicament()
        {

        }
        public void CreateValueObject(Medicament m)
        {
            this.numeMedicament = new NumeMedicament(new Text(m.strNumeMedicament));
            this.stoc = new Stoc(new Number(m.inStoc));
            this.distribuitor = new DistribuitorM(new Text(m.strDistribuitor));

            this.pretVanzare = new PretVanzare(new RealNumber(m.fPretVanzare));
            this.pretCumparare= new PretCumparare(new RealNumber(m.fPretVanzare));
            this.adresaFarmacie = new Locatie(new Text(m.strLocatie));

        }

        public void CreateDBObject()
        {
            this.strNumeMedicament = numeMedicament.getNumeMedicament.getString;
            this.inStoc = stoc.getStoc.getNumber;
            this.strDistribuitor = distribuitor.getDistribuitor.getString;
            this.fPretVanzare = pretVanzare.getPretV.getRealNumber;
            this.fPretCumparare = pretCumparare.getPretC.getRealNumber;
            this.strLocatie = adresaFarmacie.getLocatie.getString;

        }

        [Key]
        public int ID { get; set; }

        [Column("NumeMedicament")]
        [Required(ErrorMessage = "Introduceti numele")]
        public String strNumeMedicament { get; set; }

        [Column("Stoc")]
        [Required(ErrorMessage = "Introduceti stocul")]
        public int inStoc { get; set; }

        [Column("Distribuitor")]
        [Required(ErrorMessage = "Introduceti distribuitorul")]
        public String strDistribuitor { get; set; }

        [Column("PretVanzare")]
        [Required(ErrorMessage = "Introduceti numarul de telefon")]
        public float fPretVanzare { get; set; }

        [Column("PretCumparare")]
        [Required(ErrorMessage = "Introduceti email-ul")]
        public float fPretCumparare { get; set; }


        [Column("Locatie")]
        [Required(ErrorMessage = "Introduceti adresa farmaciei")]
        public String strLocatie { get; set; }
    }
}

