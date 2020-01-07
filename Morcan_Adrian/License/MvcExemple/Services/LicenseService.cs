using ContractsExample;
using MassTransit;
using LicenseWeb.Models;
using LicenseWeb.Repository;
using System;
using System.Collections.Generic;

namespace LicenseWeb.Services
{
	public interface ILicenseService
	{
		void CreateLicenses(Licenses licenses);
		List<Licenses> GetAllReservations();
		Licenses GetLicenseById(Guid id);
		void DeleteLicense(Guid id);
	}

	public class LicenseService : ILicenseService
	{
		private readonly IBusControl bus;
		private readonly ILicensesRepository repository;

		public LicenseService(IBusControl bus, ILicensesRepository repository)
		{
			this.bus = bus;
			this.repository = repository;
		}

		public void CreateLicenses(Licenses licenses)
		{
			licenses.Id = Guid.NewGuid();
			repository.CreateLicenses(licenses);

			bus.Publish(new LicenseCreated()
			{
				Id = licenses.Id,
				Name = licenses.Name,
				Description=licenses.Description,
				Category=licenses.Category,
				Quantity=licenses.Quantity
			});
		}

		public void DeleteLicense(Guid id)
		{
			var licenses = repository.GetLicenseById(id);
			repository.DeleteLicenses(licenses);

			bus.Publish(new LicenseDeleted()
			{
				Id = licenses.Id,
				Name = licenses.Name,
				Description=licenses.Description,
				Category=licenses.Category,
				Quantity=licenses.Quantity
			});
		}

		public List<Licenses> GetAllReservations()
		{
			return repository.GetAllReservations();
		}

		public Licenses GetLicenseById(Guid id)
		{
			return repository.GetLicenseById(id);
		}
	}
}
