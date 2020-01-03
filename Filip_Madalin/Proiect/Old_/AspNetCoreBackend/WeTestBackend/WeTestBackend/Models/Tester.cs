using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeTestBackend.Models
{
    public class Tester
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TesterId { get; internal set; } 
        public string TesterName { get; internal set; }
       
        //ACTUALLY  root aggregate is Test , 
        /*retain tests collection that this user is author of or tests he requested from other tested,this list will be shown in tester home page */
        public List<Test> _tests;  
        public IReadOnlyCollection<Test> TestsCollection { get { return _tests.AsReadOnly(); } }

    }
}
