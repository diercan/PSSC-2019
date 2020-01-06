using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sistem_gestiune_vanzari.Models
{
    public class ProdusModel
    {
        [DisplayName("Part Number")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        public string identitate_produs { get; set; }
        public Nullable<int> identitate_categorie { get; set; }
        [DisplayName("Nume")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        public string nume_produs { get; set; }
        public string descriere { get; set; }
        [DisplayName("Cantitate")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        public Nullable<int> cantitate { get; set; }
        [DisplayName("Cost")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> cost { get; set; }
        [DisplayName("Pret")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> pret { get; set; }
        public Nullable<int> identitate_producator { get; set; }
        public string imagine { get; set; }
        public Nullable<bool> produs_activ { get; set; }
        public Nullable<bool> produs_dezactivat { get; set; }

    }
}