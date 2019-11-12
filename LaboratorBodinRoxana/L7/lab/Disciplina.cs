using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
namespace Lab{
    public class Disciplina{
            public string denumire;
            public int nrMax;
            public int nrOcupat;
            public List<Student> list;
            public Disciplina(string d,int nrm,int nro,List<Student> l)
            {
                this.denumire=d;
                this.nrMax=nrm;
                this.nrOcupat=nro;
                this.list=l;
            }

    }
}