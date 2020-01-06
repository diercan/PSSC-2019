using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces
{
    public interface IStudent
    {
        public Student ReturnStudentAfterLoginStudent(LoginStudentViewModel student);
        public void CreateStudent(Student student);

        public IEnumerable<Student> GetAllStudents();
        
        //public static Student AddCabinet(Medic medic, string StudentAccount);

        public Medic SendStudent();


    }
}
