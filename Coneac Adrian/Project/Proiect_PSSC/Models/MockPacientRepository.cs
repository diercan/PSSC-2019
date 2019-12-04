using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.Models
{
    public class MockPacientRepository : IPacientRepository
    {
        private List<Pacient> _pacientList;

        public MockPacientRepository()
        {
            _pacientList = new List<Pacient>()
        {
            new Pacient() { Id = 1, Nume = "Popescu", Prenume = "Ion", CNP = 1980228350035, Sexul = Sex.Feminin, Adresa = "acasa"},
            new Pacient() { Id = 2, Nume= "Coneac", Prenume = "Adrian", CNP = 1987446440012, Sexul = Sex.Masculin, Adresa = "timisoara" },
            new Pacient() { Id = 3, Nume = "Vasilescu", Prenume = "Andrei", CNP = 187966440064, Sexul = Sex.Masculin, Adresa= "Sacalaz"},
        };
        }

        public Pacient Add(Pacient pacient)
        {
            pacient.Id = _pacientList.Max(e => e.Id) + 1;
            _pacientList.Add(pacient);
            return pacient;
        }

        public Pacient Delete(int id)
        {
           Pacient pacient = _pacientList.FirstOrDefault(e => e.Id == id);
            if (pacient != null)
            {
                _pacientList.Remove(pacient);
            }
            return pacient;
        }

        public IEnumerable<Pacient> GetAllPacient()
        {
            return _pacientList;
        }

        public Pacient GetPacient(int Id)
        {
            return this._pacientList.FirstOrDefault(e => e.Id == Id);
        }

        public Pacient Update(Pacient pacientChanges)
        {
            Pacient pacient = _pacientList.FirstOrDefault(e => e.Id == pacientChanges.Id);
            if (pacient != null)
            {
                pacient.Nume = pacientChanges.Nume;
                pacient.Prenume = pacientChanges.Prenume;
                pacient.CNP = pacientChanges.CNP;
                pacient.Sexul = pacientChanges.Sexul;
            }
            return pacient;
        }
    }
}
