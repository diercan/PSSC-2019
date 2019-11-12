using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
namespace Lab{
    public class Optiune{
            public Disciplina disciplina;
            public int prioritate;
            public Optiune(Disciplina d,int pr)
            {
                this.disciplina=d;
                this.prioritate=pr;
            }

    }
}