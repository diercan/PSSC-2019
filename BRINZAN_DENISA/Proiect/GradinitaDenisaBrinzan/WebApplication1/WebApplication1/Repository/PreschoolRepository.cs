using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;


namespace WebApplication1.Repository
{
    public class PreschoolRepository : IPreschool
    {
        private static List<Preschool> List;

        public PreschoolRepository()
        {
            List = new List<Preschool>();
            List.Add(new Preschool
            {
                Id = Guid.NewGuid(),
                Name = "Ayanna",
                Age = 1,
                FoodRestrictions = "gluten"
            });
            List.Add(new Preschool
            {
                Id = Guid.NewGuid(),
                Name = "Deniz",
                Age = 2,
                FoodRestrictions = "fructoza"
            });
            List.Add(new Preschool
            {
                Id = Guid.NewGuid(),
                Name = "Lara",
                Age = 3,
                FoodRestrictions = "lactoza"
            });
        }

        public void CreatePreschool(Preschool preschool)
        {
            preschool.Id = Guid.NewGuid();
            List.Add(preschool);
        }

        public IEnumerable<Preschool> GetAllPreschoolers()
        {
            return List;
        }

        public Preschool GetPreschoolById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);
        }

        public void DeletePreschool(Preschool preschool)
        {
            List.Remove(preschool);
        }

    }
}
