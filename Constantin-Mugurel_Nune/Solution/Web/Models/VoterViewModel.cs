using Data.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class VoterViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }
        [Display(Name = "Nume")]
        public string LastName { get; set; }
        [Display(Name = "CNP")]
        public string Cnp { get; set; }
        [Display(Name = "#Intrebari de verificare:")]
        public int SecretQuestionCounter { get; set; }
        public IEnumerable<SecretQuestionViewModel> SecretQuestions { get; set; }
    }
}