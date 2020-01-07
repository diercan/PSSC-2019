using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;

namespace PSSC.Models
{
    
    public class EmpDBContext : DbContext
    {
        public EmpDBContext()
        { }
        public DbSet<Car> Cars { get; set; }
    }
}