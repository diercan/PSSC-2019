using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
namespace Lab{
    public class AnUniversitar{
            public List<Student> studenti;
            public List<Disciplina> discipline;
            public AnUniversitar(List<Student> s,List<Disciplina> d) 
            {
                this.studenti=s;
                this.discipline=d;
            }

    }
}