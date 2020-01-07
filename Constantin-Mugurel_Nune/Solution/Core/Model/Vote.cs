using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Vote
    {
        public int Id { get; set; }
        public Voter Voter { get; set; }
        public Candidate Candidate { get; set; }
        public VotingSession VotingSession { get; set; }
        public DateTime SentTime { get; set; }
    }
}
