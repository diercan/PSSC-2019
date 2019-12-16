using GameRentWeb.ExceptionClasses;
using GameRentWeb.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Models
{
    public class RentDDD
    {
        private List<RentOrder> _rents;
        ReadOnlyCollection<RentOrder> Values { get { return _rents.AsReadOnly(); } }

        public RentDDD(IDataBaseRepo<RentOrder> repo)
        {
            _rents = repo.GetAllObjects().Result.ToList();
        }
        internal void AddRent(RentOrder rentToAdd, IDataBaseRepo<RentOrder> repo)
        {
            repo.Insert(rentToAdd);
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
