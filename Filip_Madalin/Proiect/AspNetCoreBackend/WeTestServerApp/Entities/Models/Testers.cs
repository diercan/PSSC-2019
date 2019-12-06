using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Testers
    {
        public Testers()
        {
            Tests = new HashSet<Tests>();
        }

        public int TesterId { get; set; }
        public string TesterName { get; set; }
        public string ProjectRole { get; set; }
        public int ProjectsProjectId { get; set; }

        public virtual Projects ProjectsProject { get; set; }
        public virtual ICollection<Tests> Tests { get; set; }
    }
}
