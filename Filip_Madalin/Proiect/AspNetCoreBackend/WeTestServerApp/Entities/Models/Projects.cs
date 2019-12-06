using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Testers = new HashSet<Testers>();
            Tests = new HashSet<Tests>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public virtual ICollection<Testers> Testers { get; set; }
        public virtual ICollection<Tests> Tests { get; set; }
    }
}
