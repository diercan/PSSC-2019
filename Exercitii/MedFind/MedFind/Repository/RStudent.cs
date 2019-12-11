using MedFind.Interfaces;
using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Repository
{
    public class RStudent : IStudent
    {
        private readonly List<Student> List_Student;
        
        public RStudent()
        {
            List_Student = new List<Student>();

            List_Student.Add(new Student { StudentAccount="Madalina.Cristina", Name="Madalina Cristina",StudentID="12345"});
            List_Student.Add(new Student { StudentAccount="Darius.Bagiu", Name = "Darius Bagiu", StudentID = "12345" });
        }

        public Student ReturnStudentAfterLoginStudent(LoginStudentViewModel student)
        {
            foreach(Student item in List_Student)
            {
                if (item.StudentAccount.Equals(student.StudentAccount))
                    return item;
                
            }
            return null;
        }


        public void CreateStudent(Student student)
        {
            List_Student.Add(student);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return List_Student;
        }
    }
}
