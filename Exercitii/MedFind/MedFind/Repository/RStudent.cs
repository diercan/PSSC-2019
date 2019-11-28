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
            List_Student.Add(new Student { Name="Mada",StudentID="a"});
            List_Student.Add(new Student { Name = "Darius", StudentID = "a" });
        }

        public Student CheckStudent(Student student)
        {
            foreach(Student item in List_Student)
            {
                if (item.Name.Equals(student.Name) && item.StudentID.Equals(student.StudentID))
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
