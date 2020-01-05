using MedFind.Models;
using MedFind.Models.ViewModels;
using MedFind.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces
{
    public interface IMedic
    {
        Medic ReturnMedicAfterLoginMedic(LoginMedicViewModel medic);

        public List<Medic> GetCabinets(string studentAccount);
        public Student SendCabinet(string MeddicAccount);

    }
}
