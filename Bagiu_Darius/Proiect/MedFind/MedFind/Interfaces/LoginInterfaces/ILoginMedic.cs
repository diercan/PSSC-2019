using MedFind.Models;
using MedFind.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces.LoginInterfaces
{
    public interface ILoginMedic
    {
        Medic CheckLoginMedic(LoginMedicViewModel loginMedic);
    }
}
