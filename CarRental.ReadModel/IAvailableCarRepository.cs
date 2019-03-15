namespace CarRental.ReadModel
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IAvailableCarRepository
	{
		Task<IList<AvailableCarReadModel>> GetAvailableCarsAsync(DateTime from, DateTime to);
	}
}
