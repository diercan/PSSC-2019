using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;

namespace scoala.Models
{
    public class Elev
    {
        public IDElev IDElev { get; set; }
        public Nume Nume { get; set; }
        public Prenume Prenume { get; set; }
        public Clasa Clasa { get; set; }

        private List<Situatie> _Situatie;
        public ReadOnlyCollection<Situatie> EleviInscrisi { get { return _Situatie.AsReadOnly(); } }



        internal Elev(Nume nume, Clasa clasa)
        {
            Contract.Requires(nume != null, "nume");
            Contract.Requires(clasa != null, "clasa");

            Nume = nume;
            Prenume = Prenume;
            Clasa = clasa;
            _Situatie = new List<Situatie>();

        }

        #region operatii
        public void NoteazaActivitateElev(IDElev Idelev, Nota nota , Materie materie)
        {
            Contract.Requires(Idelev != null);
            Contract.Requires(nota != null);
            Contract.Requires(materie != null);
           

            var elev = _Situatie.First(s => s.IDElev.Equals(Idelev));
            elev.Nota.AdaugaNota(nota);
        }

        public void PuneAbsenta(IDElev IDElev, Nume nume ,Prenume prenume, SituatieOra situatie)
        {
            Contract.Requires(IDElev != null);
            Contract.Requires(nume != null);
            Contract.Requires(prenume != null);
            Contract.Requires(situatie != null);
        }
        //absente
        #endregion

        #region override object
        public override string ToString()
        {
            return Nume.ToString();
        }

        public override bool Equals(object obj)
        {
            var elev = (Elev)obj;

            if (elev != null)
            {
                return Nume.Equals(elev.Nume);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }
        #endregion
    }
}




    
