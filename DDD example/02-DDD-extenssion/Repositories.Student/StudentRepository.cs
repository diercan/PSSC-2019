using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Student
{
    public class StudentRepository
    {
        public static readonly List<Modele.Student.Student> _studenti = new List<Modele.Student.Student>();

        public Modele.Student.Student IncarcaStudent(NumarMatricol numarMatricol)
        {
            return _studenti.SingleOrDefault(s => s.NumarMatricol.Equals(numarMatricol));
        }

        public void SalvareStudent(Modele.Student.Student student)
        {
            var studentGasit = IncarcaStudent(student.NumarMatricol);
            if(student!=null) _studenti.Remove(studentGasit);
            _studenti.Add(student);
        }
    }
}
