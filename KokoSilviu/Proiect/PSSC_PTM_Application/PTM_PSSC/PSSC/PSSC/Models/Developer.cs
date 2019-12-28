using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public class Developer
    {
        public string name { get; private set; }
        public string internal_id { get; private set; }      


        public void UpdateDeveloper(Developer d)
        {
            this.name = d.name;
            this.internal_id = d.internal_id;
        }

        public void UpdateInternalID(string id)
        {
            this.internal_id = id;
        }

        public void UpdateName(string n)
        {
            this.name = n;
        }
    }
}
