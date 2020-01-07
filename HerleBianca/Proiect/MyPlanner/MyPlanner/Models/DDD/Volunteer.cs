using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Models.DDD
{
    public class Volunteer
    {
        string _nume;
        Guid _id;
        public string Nume { get { return _nume; } }
        public Guid Id { get { return _id; } }
        public Volunteer(string nume)
        {
            _nume = nume;
            _id = new Guid();
        }
        #region override object
        public override string ToString()
        {
            return Nume;
        }

        public override bool Equals(object obj)
        {
            var volunteer = (Volunteer)obj;
            return Nume.Equals(volunteer.Nume) && Id.Equals(volunteer.Id);
        }

        public override int GetHashCode()
        {
            return Nume.GetHashCode();
        }
        #endregion
    }
}
