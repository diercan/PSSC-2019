using LicenseWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LicenseWeb.Repository
{
	public interface ILicensesRepository
	{
		void CreateLicenses(Licenses licenses);
		List<Licenses> GetAllReservations();
		Licenses GetLicenseById(Guid id);
		void DeleteLicenses(Licenses licenses);
	}

	public class LicenseRepository : ILicensesRepository
	{
		private readonly List<Licenses> List;

		public LicenseRepository()
		{
			List = new List<Licenses>();
			List.Add(new Licenses
			{
				Id = Guid.NewGuid(),
				Name = "Eclipse",
				Description = "IDE=>C/C++ or Java",
				Category = "Tools",
				Quantity = 4
			}) ;
			List.Add(new Licenses
			{
				Id = Guid.NewGuid(),
				Name = "Fifa",
				Description = "PC/PS4/Xbox game",
				Category = "Games",
				Quantity = 1
			}) ;
			
		}

		public void CreateLicenses(Licenses licensesadd)
		{
			List.Add(licensesadd);
		}

		public void DeleteLicenses(Licenses licensesdelete)
		{
			List.Remove(licensesdelete);
		}

		public List<Licenses> GetAllReservations()
		{
			return List;
		}

		public Licenses GetLicenseById(Guid id)
		{
			return List.FirstOrDefault(_ => _.Id == id);
		}
	}
}
