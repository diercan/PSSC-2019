using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IPreschool
    {
        public void CreatePreschool(Preschool preschool);
        public IEnumerable<Preschool> GetAllPreschoolers();

        public Preschool GetPreschoolById(Guid id);
        public void DeletePreschool(Preschool preschool);
    }
}
