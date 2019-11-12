using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
namespace Lab{
    public class Serviciu{
          public void Repartizare(AnUniversitar an)
          {
                List<Studenti> studentiSortati=an.studenti;
                studentiSortati.Sort();
                foreach(Student s in studentiSortati)
                {
                    List<Optiune> opt=s.opt;
                    foreach(Optiune o in opt)
                    {
                        if(o.prioritate==1)
                        {
                            if(o.disciplina.nrOcupat<=o.disciplina.nrMax)
                            {
                                o.list.Add(s);
                                o.disciplina.nrOcupat++;
                           }
                            else if(o.prioritate==2)
                            {
                                
                            }
                        
                    }
                }
          }
        }
    }
}