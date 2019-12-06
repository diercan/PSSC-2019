using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Functionality
    {
        public Functionality()
        {
            Tests = new HashSet<Tests>();
        }

        public int FunctionalityId { get; set; }
        public string FunctionalityName { get; set; }

        public virtual ICollection<Tests> Tests { get; set; }
    }
}
