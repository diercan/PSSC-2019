using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public string GoodFeedback { get; set; }
        public string BadFeedback { get; set; }


    }
}
