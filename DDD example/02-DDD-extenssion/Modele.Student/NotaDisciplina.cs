using Modele.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele.Student
{
    public class NotaDisciplina
    {
        public Nota Nota { get; private set; }
        public DateTime Data { get; private set; }

        public NotaDisciplina(Nota nota, DateTime data)
        {
            Nota = nota;
            Data = data;
        }
    }
}
