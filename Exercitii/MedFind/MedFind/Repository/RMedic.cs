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
        
        private string LoggedStudent;
        public RMedic()
        {
            List_Medics = new List<Medic>();
            List_Medics.Add(new Medic { MedicAccount = "Medic1", Name = "George", Specialty = Category.Endodontie, Description = "descriere cabinet 1" });
            List_Medics.Add(new Medic { MedicAccount = "Medic2", Name = "Georgel", Specialty = Category.Parodontologie, Description = "descriere cabinet 2" });
            List_Medics.Add(new Medic { MedicAccount = "Medic3", Name = "Andrei", Specialty = Category.Prostetica_dentara, Description = "descriere cabinet 3"});
            List_Medics.Add(new Medic { MedicAccount = "Medic4", Name = "Mircea", Specialty = Category.Chirurgia_Dento_Alveolara, Description = "descriere cabinet 4" });
            List_Medics.Add(new Medic { MedicAccount = "Medic5", Name = "Abrudan", Specialty = Category.Chirurgia_Oro_Maxilo_Faciala, Description = "descriere cabinet 5" });

        }

        public List<Medic> GetCabinets(string studentAccount)
        {
            LoggedStudent = studentAccount;
            return List_Medics;
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

        public Student SendCabinet(string MedicAccount)
        {
            foreach (Medic item in List_Medics)
            {
                if (item.MedicAccount.Equals(MedicAccount))
                {
                    RStudent rstudent= new RStudent();
                    
                    return rstudent.AddCabinet(item, LoggedStudent);
                }
            }
            throw new NotImplementedException();
        }

     

        Medic IMedic.ReturnMedicAfterLoginMedic(LoginMedicViewModel medic)
        {
            throw new NotImplementedException();
        }
    }
}
