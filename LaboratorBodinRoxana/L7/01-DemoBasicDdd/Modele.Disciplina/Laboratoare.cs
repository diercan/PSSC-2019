using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Modele.Disciplina
{
    public class Laboratoare
    {
        private List<Laborator> _laboratoare;
        public ReadOnlyCollection<Laborator> Valori { get { return _laboratoare.AsReadOnly(); } }

        internal Laboratoare()
        {
            _laboratoare = new List<Laborator>();
        }

        internal Laboratoare(List<Laborator> laboratoare){
            //Contract.Requires(laboratoare != null, "lista de laboratoare");
            _laboratoare = laboratoare;
        }

        internal void AdaugaLaborator(Laborator laborator){
            //Contract.Requires(laborator != null, "laborator");
            _laboratoare.Add(laborator);
        }

        #region override object
        public override string ToString()
        {
            return _laboratoare.Aggregate(new StringBuilder(), (builder, laborator) => {
                if (builder.Length > 0) builder.Append(", ");
                builder.Append(laborator);
                return builder;
            }).ToString();

        }

        public override bool Equals(object obj)
        {
            var laboratoare = (Laboratoare)obj;

            if (laboratoare != null && laboratoare._laboratoare.Count == _laboratoare.Count)
            {
                return _laboratoare.Select((laborator, idx) => new {Laborator1 = laborator, Laborator2 = laboratoare._laboratoare[idx]})
                    .Aggregate(true, (equal, pair)=> equal && pair.Laborator1.Equals(pair.Laborator2));

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
