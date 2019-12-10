using MedFind.Interfaces.LoginInterfaces;
using MedFind.Models;
using MedFind.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Repository.LoginRepository
{
    public class RLoginMedic: ILoginMedic
    {
        private readonly List<LoginMedicViewModel> list_accounts;
        private readonly RMedic List_Medics = new RMedic();
        public RLoginMedic()
        {
            list_accounts = new List<LoginMedicViewModel>();
            list_accounts.Add(new LoginMedicViewModel { MedicAccount = "Medic1", Password = "p" });
            list_accounts.Add(new LoginMedicViewModel { MedicAccount = "Medic2", Password = "p" });
            list_accounts.Add(new LoginMedicViewModel { MedicAccount = "Medic3", Password = "p" });
            list_accounts.Add(new LoginMedicViewModel { MedicAccount = "Medic4", Password = "p" });
            list_accounts.Add(new LoginMedicViewModel { MedicAccount = "Medic5", Password = "p" });
        }

        public Medic CheckLoginMedic(LoginMedicViewModel loginMedic)
        {
            foreach (LoginMedicViewModel login in list_accounts)
            {
                if (login.Password.Equals(loginMedic.Password) && login.MedicAccount.Equals(loginMedic.MedicAccount))
                {
                    var model = List_Medics.ReturnMedicAfterLoginMedic(login);

                    return model;

                }
            }
            return null;
        }
    }
}
