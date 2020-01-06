using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari.Models
{
    public class CategorieModel
    {
        public int identitate_categorie { get; set; }
        [DisplayName("Categorie")]
        [Required(ErrorMessage = "Campul trebuie completat")]
        public string nume_categorie { get; set; }
        public Nullable<bool> categorie_activa { get; set; }
        public Nullable<bool> categorie_stearsa { get; set; }
    }
}