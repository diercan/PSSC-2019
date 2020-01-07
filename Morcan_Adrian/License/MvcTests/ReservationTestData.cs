using LicenseWeb.Models;
using System;
using System.Collections.Generic;

namespace MvcTests
{
	public class ReservationTestData
	{
		public static List<Licenses> Reservations = new List<Licenses>()
		{
			new Licenses
			{
				Id = Guid.NewGuid(),
				Name = "McAfee",
				Description="program",
				Category="antivirus",
				Quantity=2
			},
			new Licenses
			{
				Id = Guid.NewGuid(),
				Name = "Visual Studio",
				Description="IDE",
				Category="Tools",
				Quantity=1
			},
		};
	}
}
