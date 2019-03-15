namespace CarRental.Api.Infrastructure.Services
{
	using CarRental.ReadModel;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IAvailableCarService
	{
		Task<IList<AvailableCarReadModel>> FindAvailableCarsAsync(DateTime from, DateTime to);
	}
}
