﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Models
{
    public class RentOrder
    {
        public int Id { get; set; }
        public DateTime CurrentRentedDay { get; set; }  // the day the rent order was placed
        [Required]
        [Range(0,30,ErrorMessage ="The number of days must be between 0 and 30")]
        [Display(Name = "Rent periond(in days)")]
        public int RentPeriod { get; set; } // the number of days
        public DateTime ExpiringDate { get; set; }      // the date when the game must be returned
        public float TotalPayment { get; set; }     // will be set based on the number of rent days 
        public string GameRented { get; set; }
        public virtual User user { get; set; }

        #region operations
        public async Task<RentOrder> CreateRentAsync(MessageBroker broker)
        {
            CurrentRentedDay = DateTime.Today;
            // get current game

            var rentJson = JsonConvert.SerializeObject(this);
            await broker.SendMessage(rentJson, "RentToWorker");
            var rentReceived = broker.ReceiveMessage("WorkerToRent").Result;

            ExpiringDate = rentReceived.ExpiringDate;
            TotalPayment = rentReceived.TotalPayment;

            return this;
        }

        public async Task<RentOrder> InterruptRentAsync(MessageBroker broker)
        {
            var selectedRentJson = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
           );
            await broker.SendMessage(selectedRentJson, "ReturnToWorker");
            var receivedRent = await broker.ReceiveMessage("ReturnToWeb");
            return this;
        }

        public async Task<RentOrder> ExtendRentAsync(int days,MessageBroker broker)
        {
            this.RentPeriod += Convert.ToInt32(days);

            var selectedRentJson = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            );

            await broker.SendMessage(selectedRentJson, "RentToWorker");
            var rentReceived = broker.ReceiveMessage("WorkerToRent").Result;
            return rentReceived;
        }
        #endregion

    }
}
