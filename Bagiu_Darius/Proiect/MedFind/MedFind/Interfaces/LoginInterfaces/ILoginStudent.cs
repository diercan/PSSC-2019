using MedFind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Interfaces
{
    public interface ILoginStudent
    {
        Student CheckLoginStudent(LoginStudentViewModel loginStudent);
    }
}
