using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Tests
    {
        public int TestId { get; set; }
        public string TestTitle { get; set; }
        public string TestDescription { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Functionality { get; set; }
        public int? TesterId { get; set; }
        public string RevisionHistory { get; set; }
        public int ProjectsProjectId { get; set; }
        public int FunctionalityFunctionalityId { get; set; }
        public int TestersTesterId { get; set; }

        public virtual Functionality FunctionalityFunctionality { get; set; }
        public virtual Projects ProjectsProject { get; set; }
        public virtual Testers TestersTester { get; set; }
    }
}
