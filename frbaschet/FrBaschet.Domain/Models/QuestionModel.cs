using System;
using System.Collections.Generic;

namespace FrBaschet.Domain.Models
{
    public class QuestionModel : ValueObject
    {
        public string Question { get; set; }
        public bool Answer { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}