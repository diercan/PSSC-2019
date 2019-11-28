using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces
{
    public interface IStudent
    {
        Student CheckStudent(Student student);
        void CreateStudent(Student student);

        IEnumerable<Student> GetAllStudents();
    }
}
