namespace CarRental.Data.UnitOfWork.Mapping
{
	using CarRental.Domain.Aggregates.CarAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
	using System;
	using System.Collections.Generic;

	public class CarConfiguration : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.HasOne(c => c.Type).WithMany().HasForeignKey(c => c.TypeId);
			builder.Property(c => c.TransmissionType).HasConversion<string>();

			builder.Property(c => c.HasAirConditioning).HasConversion(new BoolToZeroOneConverter<Int16>());
			builder.Property(c => c.HasPetrol).HasConversion(new BoolToZeroOneConverter<Int16>());
			builder.Property(c => c.HasDiesel).HasConversion(new BoolToZeroOneConverter<Int16>());

			builder.HasData(SeedCars());
		}

		private static List<Car> SeedCars() {
			var cars = new List<Car>();

			var car = new Car
			{
				Name = "Volkswagen Up!",
				NumberOfSeats = 4,
				NumberOfBags = 1,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Manual,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = false,
				ImageUrl = "assets/images/VolkswagenUp.png"
			};
			car.SetTheTypeReference(1);
			car.ChangeCurrentIdentity(1);
			cars.Add(car);

			car = new Car
			{
				Name = "Volkswagen Polo",
				NumberOfSeats = 5,
				NumberOfBags = 2,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Manual,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = true,
				ImageUrl = "assets/images/VolkswagenPolo.png"
			};
			car.SetTheTypeReference(1);
			car.ChangeCurrentIdentity(2);
			cars.Add(car);

			car = new Car
			{
				Name = "Renault Clio Automatic",
				NumberOfSeats = 5,
				NumberOfBags = 2,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Automatic,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = false,
				ImageUrl = "assets/images/RenaultClioAutomatic.png"
			};
			car.SetTheTypeReference(1);
			car.ChangeCurrentIdentity(3);
			cars.Add(car);

			car = new Car
			{
				Name = "Volkswagen Golf",
				NumberOfSeats = 5,
				NumberOfBags = 2,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Manual,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = true,
				ImageUrl = "assets/images/VolkswagenGolf.png"
			};
			car.SetTheTypeReference(2);
			car.ChangeCurrentIdentity(4);
			cars.Add(car);

			car = new Car
			{
				Name = "Volkswagen Golf Automatic",
				NumberOfSeats = 5,
				NumberOfBags = 2,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Automatic,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = true,
				ImageUrl = "assets/images/VolkswagenGolfAutomatic.png"
			};
			car.SetTheTypeReference(2);
			car.ChangeCurrentIdentity(5);
			cars.Add(car);

			car = new Car
			{
				Name = "Dacia Duster 4x4",
				NumberOfSeats = 5,
				NumberOfBags = 3,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Manual,
				HasAirConditioning = true,
				HasPetrol = true,
				HasDiesel = true,
				ImageUrl = "assets/images/DaciaDuster.png"
			};
			car.SetTheTypeReference(2);
			car.ChangeCurrentIdentity(6);
			cars.Add(car);

			car = new Car
			{
				Name = "Mercedes-Benz C-Class Automatic",
				NumberOfSeats = 5,
				NumberOfBags = 2,
				NumberOfDoors = 4,
				TransmissionType = Car.CarTransmissionType.Automatic,
				HasAirConditioning = true,
				HasPetrol = false,
				HasDiesel = true,
				ImageUrl = "assets/images/MercedesBenzC.png"
			};
			car.SetTheTypeReference(3);
			car.ChangeCurrentIdentity(7);
			cars.Add(car);

			car = new Car
			{
				Name = "Mercedes Benz GLC 220 Automatic Diesel",
				NumberOfSeats = 5,
				NumberOfBags = 3,
				NumberOfDoors = 4,
				TransmissionType = Car.CarTransmissionType.Automatic,
				HasAirConditioning = true,
				HasPetrol = false,
				HasDiesel = true,
				ImageUrl = "assets/images/MercedesBenzGLC.png"
			};
			car.SetTheTypeReference(3);
			car.ChangeCurrentIdentity(8);
			cars.Add(car);

			car = new Car
			{
				Name = "Mercedes-Benz GLE 250 Automatic Diesel",
				NumberOfSeats = 5,
				NumberOfBags = 3,
				NumberOfDoors = 5,
				TransmissionType = Car.CarTransmissionType.Automatic,
				HasAirConditioning = true,
				HasPetrol = false,
				HasDiesel = true,
				ImageUrl = "assets/images/MercedesBenzGLE.png"
			};
			car.SetTheTypeReference(3);
			car.ChangeCurrentIdentity(9);
			cars.Add(car);

			return cars;
		}
	}
}
