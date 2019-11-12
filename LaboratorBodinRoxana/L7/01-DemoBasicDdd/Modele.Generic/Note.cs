using Modele.Generic.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Generic
{
    public class Note
    {
        private List<Nota> _note;
        public ReadOnlyCollection<Nota> Valori { get { return _note.AsReadOnly(); } }

        public Note()
        {
            _note = new List<Nota>();
        }

        //acest constructor este internal pentru ca ar trebui sa fie folosit doar din repository
        internal Note(List<Nota> note){
            //Contract.Requires(note != null, "lista de note");
            _note = note;
        }

        public Nota Media
        {
            get {
                if (_note.Count < 2) throw new NoteInsuficienteException();
                return new Nota(_note.Select(n => n.Valoare).Average());
            }
        }

        internal void AdaugaNota(Nota nota){
            //Contract.Requires(nota != null, "nota");
            _note.Add(nota);
        }

        #region override object
        public override string ToString()
        {
            return _note.Aggregate(new StringBuilder(), (builder, nota) => {
                if (builder.Length > 0) builder.Append(", ");
                builder.Append(nota);
                return builder;
            }).ToString();

        }

        public override bool Equals(object obj)
        {
            var note = (Note)obj;

            if (note != null && note._note.Count == _note.Count)
            {
                return _note.Select((nota, idx) => new {Nota1 = nota, Nota2 = note._note[idx]})
                    .Aggregate(true, (equal, pair)=> equal && pair.Nota1.Equals(pair.Nota2));

            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}
