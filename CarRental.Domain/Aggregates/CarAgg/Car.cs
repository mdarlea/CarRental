namespace CarRental.Domain.Aggregates.CarAgg
{
	using CarRental.Domain.Aggregates.CarTypeAgg;
	using Swaksoft.Domain.Seedwork.Aggregates;
	using System;
	using System.ComponentModel.DataAnnotations;
	
	public class Car: Entity
	{
		public enum CarTransmissionType {
			Manual,
			Automatic
		}

		[Required]
		[MaxLength(250)]
		public string Name { get; set; }

		[Required]
		public CarTransmissionType TransmissionType { get; set; }
				
		public int NumberOfDoors { get; set; }

		public int NumberOfBags { get; set; }

		public int NumberOfSeats { get; set; }

		public bool HasPetrol { get; set; }

		public bool HasDiesel { get; set; }

		public bool HasAirConditioning { get; set; }

		[MaxLength(300)]
		public string ImageUrl { get; set; }

		public int TypeId { get; private set; }
		public virtual CarType Type { get; private set; }

		public void SetTheTypeForThisCar(CarType carType)
		{
			if (carType == null || carType.IsTransient())
			{
				throw new ArgumentException("Cannot associate transient or null car type");
			}

			TypeId = carType.Id;
			Type = carType;
		}

		public void SetTheTypeReference(int carTypeId)
		{
			if (carTypeId > 0)
			{				
				TypeId = carTypeId;

				Type = null;
			}
		}
	}
}
