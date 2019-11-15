using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericWorker
{
    class RentOperations
    {
        private RentOrder _rentOrder;
        public RentOperations(RentOrder rentOrder)
        {
            _rentOrder = rentOrder;
        }

     
      
        public async Task<RentOrder> CalculatePayment(float dollarPerDay)
        {
            _rentOrder.ExpiringDate = _rentOrder.CurrentRentedDay.AddDays(_rentOrder.RentPeriod);
            _rentOrder.TotalPayment = _rentOrder.RentPeriod * dollarPerDay;
            return _rentOrder;
        }

        public async Task<RentOrder> CalculateReturn(float dollarPerDay)
        {
            _rentOrder.ExpiringDate = DateTime.Today;
            _rentOrder.RentPeriod = Convert.ToInt32((_rentOrder.ExpiringDate - _rentOrder.CurrentRentedDay).TotalDays);
            _rentOrder.TotalPayment -= _rentOrder.RentPeriod * dollarPerDay;       
            return _rentOrder;
        }
    }
}
