namespace CarRental.Data.UnitOfWork.Mapping
{
	using CarRental.Domain.Aggregates.CarTypeAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;	
	using System.Collections.Generic;

	public class CarTypeConfiguration : IEntityTypeConfiguration<CarType>
	{
		public void Configure(EntityTypeBuilder<CarType> builder)
		{
			builder.HasData(SeedCarTypes());
		}

		private static List<CarType> SeedCarTypes() {
			var carTypes = new List<CarType>();

			var type = new CarType
			{
				Type = "Small"
			};
			type.ChangeCurrentIdentity(1);
			carTypes.Add(type);

			type = new CarType
			{
				Type = "Mid-Size"
			};
			type.ChangeCurrentIdentity(2);
			carTypes.Add(type);

			type = new CarType
			{
				Type = "Large"
			};
			type.ChangeCurrentIdentity(3);
			carTypes.Add(type);

			return carTypes;
		}
	}
}
