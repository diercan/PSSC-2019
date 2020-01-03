using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Test is the root aggregate for WeTest app context
/// not quite ddd but yea #trying
/// </summary>
namespace WeTestBackend.Models
{
    
    public class Test
    {
        /*
         * TO DO 
         * test id should be a a pattern like Discipline_ProjectName_UniqueID for traceability 
         * #realised i dont need project entity .... all tests  will have the project acronim in id  
         * so if i want to filter by project i will make a string match search on id
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TestId { get; internal set; } 
        public string TestTitle { get; internal set; }
        public virtual Tester Author { get; internal set; }

        ///*retain testers that used this test in different  projects */
        //public List<Tester> _testersHistory;

        ///*make it read only to lock  access conform ddd*/
        //public IReadOnlyCollection<Tester> TestersHistory { get {return  _testersHistory.AsReadOnly(); } }

        public Functionality Functionality { get; internal set; }

        /*empty constructor for EF core ??*/
        



        

    }
}
