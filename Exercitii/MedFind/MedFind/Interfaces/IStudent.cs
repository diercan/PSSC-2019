using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces
{
    public interface IStudent
    {
        Student ReturnStudentAfterLoginStudent(LoginStudentViewModel student);
        void CreateStudent(Student student);

        IEnumerable<Student> GetAllStudents();
        
        Student AddCabinet(Medic medic, string StudentAccount);
    }
}
