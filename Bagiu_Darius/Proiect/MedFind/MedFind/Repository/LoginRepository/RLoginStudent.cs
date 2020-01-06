using MedFind.Interfaces;
using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Repository
{
    public class RLoginStudent : ILoginStudent
    {
        private readonly List<LoginStudentViewModel> list_accounts;
        private RStudent List_Student = new RStudent();

        public RLoginStudent()
        {
            list_accounts = new List<LoginStudentViewModel>();
            list_accounts.Add(new LoginStudentViewModel { StudentAccount = "Darius.Bagiu", Password = "a" });
            list_accounts.Add(new LoginStudentViewModel { StudentAccount = "Alexandru.Abrudan", Password = "a" });
            list_accounts.Add(new LoginStudentViewModel { StudentAccount = "Madalina.Cristina", Password = "a" });
            
        }

        

        public Student CheckLoginStudent(LoginStudentViewModel loginStudent)
        {
            foreach(LoginStudentViewModel login  in list_accounts)
            {
                if(login.Password.Equals(loginStudent.Password) && login.StudentAccount.Equals(loginStudent.StudentAccount))
                {
                    var model =  List_Student.ReturnStudentAfterLoginStudent(login);
                   
                    return model;

                }
            }
            return null;
        }
    }
}
