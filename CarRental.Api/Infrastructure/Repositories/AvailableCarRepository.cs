namespace CarRental.Api.Infrastructure.Repositories
{
	using CarRental.Api.Models;
	using CarRental.Data.UnitOfWork;
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class AvailableCarRepository : IAvailableCarRepository
	{
		private readonly CarRentalUnitOfWork unitOfWork;

		public AvailableCarRepository(CarRentalUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}
		public async Task<IList<AvailableCarReadModel>> GetAvailableCarsAsync(DateTime from, DateTime to)
		{
			var now = DateTime.Now;
			var grouped = from availableCar in unitOfWork.AvailableCars
						  join car in unitOfWork.Cars
						   on availableCar.CarId equals car.Id
						  join carType in unitOfWork.CarTypes
							on car.TypeId equals carType.Id
						  join booking in unitOfWork.Bookings
							on availableCar.Id equals booking.AvailableCarId into bookings
						  from b in bookings.DefaultIfEmpty()
						  where b == null || ((b.To >= now && b.To >= @from && b.To <= to) || (b.From >= @from && b.From <= to))
						  let hasBookings = (b == null)
						  group b by						  
						  new
						  {							  
							  CarId = car.Id,
							  car.Name,
							  TransmissionType = car.TransmissionType.ToString(),
							  car.NumberOfDoors,
							  car.NumberOfBags,
							  car.NumberOfSeats,
							  car.HasPetrol,
							  car.HasDiesel,
							  car.HasAirConditioning,
							  car.ImageUrl,
							  CarTypeId = carType.Id,
							  carType.Type,
							  availableCar.Id,
							  availableCar.Price,
							  availableCar.NumberOfCars
						  }
						  into g						 
						  select new AvailableCarReadModel
						  {
							  CarId = g.Key.CarId,
							  Name = g.Key.Name,
							  TransmissionType = g.Key.TransmissionType,
							  NumberOfDoors = g.Key.NumberOfDoors,
							  NumberOfBags = g.Key.NumberOfBags,
							  NumberOfSeats = g.Key.NumberOfSeats,
							  HasPetrol = g.Key.HasPetrol,
							  HasDiesel = g.Key.HasDiesel,
							  HasAirConditioning = g.Key.HasAirConditioning,
							  ImageUrl = g.Key.ImageUrl,
							  CarTypeId = g.Key.CarTypeId,
							  Type = g.Key.Type,
							  Id = g.Key.Id,
							  Price = g.Key.Price,
							  AvailableCars = g.Key.NumberOfCars - g.Count()
						  };

			return await grouped.Where(ac => ac.AvailableCars > 0).ToListAsync();
		}		
	}
}
