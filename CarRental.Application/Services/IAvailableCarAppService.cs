namespace CarRental.Application.Services
{
	using CarRental.ReadModel;
	using System;
	using System.Threading.Tasks;

	public interface IAvailableCarAppService : IDisposable
	{
		Task<AvailableCarResult> FindAvailableCarById(int id);
	}
}
