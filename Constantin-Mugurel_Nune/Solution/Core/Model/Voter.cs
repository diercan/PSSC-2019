using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Voter
    {
        public Voter()
        {
            SecretQuestions = new List<SecretQuestion>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnp { get; set; }
        public ICollection<SecretQuestion> SecretQuestions { get; set; }

    }
}
