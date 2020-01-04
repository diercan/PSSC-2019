using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public class Developer
    {
        public string internal_id { get; private set; }      

        public Developer(string id)
        {
            this.internal_id = id;
        }

        public Developer()
        {

        }

        public void UpdateInternalID(string id)
        {
            this.internal_id = id;
        }

    }
}
