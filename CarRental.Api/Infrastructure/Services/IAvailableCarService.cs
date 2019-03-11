namespace CarRental.Api.Infrastructure.Services
{
	using CarRental.Api.Models;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public interface IAvailableCarService
	{
		Task<IList<AvailableCarReadModel>> FindAvailableCarsAsync(DateTime from, DateTime to);
	}
}
