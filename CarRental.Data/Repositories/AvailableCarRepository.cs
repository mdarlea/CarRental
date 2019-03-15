namespace CarRental.Data.Repositories
{
	using CarRental.Data.UnitOfWork;
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using Microsoft.EntityFrameworkCore;
	using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
	using System;
	using System.Threading.Tasks;

	public class AvailableCarRepository : Repository<AvailableCar>, IAvailableCarRepository
	{
		private readonly CarRentalUnitOfWork unitOfWork;

		public AvailableCarRepository(CarRentalUnitOfWork unitOfWork) : base(unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public async Task<AvailableCar> GetAvailableCar(int id)
		{
			return await unitOfWork.AvailableCars
							.Include(ac => ac.Car)
							.ThenInclude(c => c.Type)
							.SingleOrDefaultAsync(ac => ac.Id == id);
		}
	}
}
