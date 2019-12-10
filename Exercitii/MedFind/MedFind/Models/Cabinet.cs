using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class Cabinet
    {
        public Category Specialty { get;set; }

        public string Description { get; set; }

    }
    public enum Category
    {
        Endodontie,
        Parodontologie,
        Protetica_dentara,
        Chirurgia_Oro_Maxilo_Faciala,
        Chirurgia_Dento_Alveolara,
    }
}
