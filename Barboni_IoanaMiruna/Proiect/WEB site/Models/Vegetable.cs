using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegumeDeBelint.Models
{
    public class Vegetable
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Price
        {
            get;
            set;
        }

        public string Photo
        {
            get;
            set;
        }
    }
}