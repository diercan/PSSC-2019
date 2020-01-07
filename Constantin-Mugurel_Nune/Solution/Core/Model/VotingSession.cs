using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class VotingSession
    {
        public VotingSession()
        {
            Candidates = new HashSet<Candidate>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}
