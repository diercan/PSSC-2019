using MvcExample.Models;
using System;
using System.Collections.Generic;

namespace MvcTests
{
	public class ReservationTestData
	{
		public static List<Reservation> Reservations = new List<Reservation>()
		{
			new Reservation
			{
				Id = Guid.NewGuid(),
				Date = DateTime.Now,
				Name = "Tudor",
				WashMachineType = WashMachineType.Gorenje
			},
			new Reservation
			{
				Id = Guid.NewGuid(),
				Date = DateTime.Now,
				Name = "Tudor2",
				WashMachineType = WashMachineType.Bosh
			}
		};
	}
}
