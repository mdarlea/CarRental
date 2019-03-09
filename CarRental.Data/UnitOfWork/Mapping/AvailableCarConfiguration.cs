namespace CarRental.Data.UnitOfWork.Mapping
{
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using System.Collections.Generic;

	public class AvailableCarConfiguration : IEntityTypeConfiguration<AvailableCar>
	{
		public void Configure(EntityTypeBuilder<AvailableCar> builder)
		{
			builder.HasOne(ac => ac.Car).WithMany().HasForeignKey(ac => ac.CarId);
			builder.HasData(SeedAvailableCars());			
		}

		private static List<AvailableCar> SeedAvailableCars() {
			var availableCars = new List<AvailableCar>();

			var availableCar = new AvailableCar
			{
				Price = 85.99M,
				NumberOfCars = 10
			};
			availableCar.SetTheCarReference(1);
			availableCar.ChangeCurrentIdentity(1);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 87.99M,
				NumberOfCars = 15
			};
			availableCar.SetTheCarReference(2);
			availableCar.ChangeCurrentIdentity(2);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 94.01M,
				NumberOfCars = 5
			};
			availableCar.SetTheCarReference(3);
			availableCar.ChangeCurrentIdentity(3);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 112.00M,
				NumberOfCars = 7
			};
			availableCar.SetTheCarReference(4);
			availableCar.ChangeCurrentIdentity(4);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 118.00M,
				NumberOfCars = 12
			};
			availableCar.SetTheCarReference(5);
			availableCar.ChangeCurrentIdentity(5);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 135.99M,
				NumberOfCars = 13
			};
			availableCar.SetTheCarReference(6);
			availableCar.ChangeCurrentIdentity(6);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 276.01M,
				NumberOfCars = 5
			};
			availableCar.SetTheCarReference(7);
			availableCar.ChangeCurrentIdentity(7);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 308.00M,
				NumberOfCars = 9
			};
			availableCar.SetTheCarReference(8);
			availableCar.ChangeCurrentIdentity(8);
			availableCars.Add(availableCar);

			availableCar = new AvailableCar
			{
				Price = 352.00M,
				NumberOfCars = 4
			};
			availableCar.SetTheCarReference(9);
			availableCar.ChangeCurrentIdentity(9);
			availableCars.Add(availableCar);

			return availableCars;
		}
	}
}
