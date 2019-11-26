using System;
using System.ComponentModel.DataAnnotations;

namespace MvcExample.Models
{
	public class Reservation
	{
		public Guid Id { get; set; }

		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		public WashMachineType WashMachineType { get; set; }

		[Required]
		public string Name { get; set; }
	}

	public enum WashMachineType
	{
		Bosh,
		Samsung,
		Beko,
		Gorenje,
		Arctic
	}
}
