using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace TeacherMVC.Models
{
    public class FeedbackReceive
    {
        public Guid Id { get; set; }
        public string GoodFeedback { get; set; }

    }
}
