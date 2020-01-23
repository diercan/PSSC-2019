using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkinShopCSGO.Models
{
    public class Skin
    {
        [Key]
        public int SkinId { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Collection { get; set; }
    }
}