using MedFind.Interfaces;
using MedFind.Models;
using MedFind.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Repository
{
    public class RMedic : IMedic
    {
        private readonly List<Medic> List_Medics;
        public RMedic()
        {
            List_Medics = new List<Medic>();
            List_Medics.Add(new Medic { MedicAccount = "Medic1", Name = "George", CabinetMedic = new Cabinet {Specialty = Category.Endodontie, Description = "descriere cabinet 1" } });
        }
        public Medic ReturnMedicAfterLoginMedic(LoginMedicViewModel medic)
        {
            foreach (Medic item in List_Medics)
            {
                if (item.MedicAccount.Equals(medic.MedicAccount))
                    return item;

            }
            return null;
        }

        Medic IMedic.ReturnMedicAfterLoginMedic(LoginMedicViewModel medic)
        {
            throw new NotImplementedException();
        }
    }
}
