using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkinShopCSGO.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int ClientId { get; set; }
        public int SkinId { get; set; }
        [Display(Name = "Loan date"), DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Skin Skin { get; set; }
    }
}