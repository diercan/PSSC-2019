using GameRentWeb.ExceptionClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Models
{
    public class Rents
    {
        private List<RentOrder> _rents;
        public ReadOnlyCollection<RentOrder> Values { get { return _rents.AsReadOnly(); } }
        public Rents()
        {
            _rents = new List<RentOrder>();
        }
        internal void AddRentList(RentOrder rentToAdd)
        {
            _rents.Add(rentToAdd);
        }

        public float TotalPaymentRents
        {
            get {
                if (_rents.Count < 1)
                {
                    throw new NotEnoughRents(0);
                }
                else
                {
                    return _rents.Sum<RentOrder>(r => r.TotalPayment);
                }
            }
        }
    }
}
