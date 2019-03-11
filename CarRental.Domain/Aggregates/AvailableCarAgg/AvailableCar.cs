namespace CarRental.Domain.Aggregates.AvailableCarAgg
{
	using CarRental.Domain.Aggregates.BookingAgg;
	using CarRental.Domain.Aggregates.CarAgg;
	using Swaksoft.Domain.Seedwork.Aggregates;
	using System;
	using System.Collections.Generic;

	public class AvailableCar: Entity
	{
		public int CarId { get; private set; }
		public virtual Car Car { get; private set; }

		public int NumberOfCars { get; set; }

		public decimal Price { get; set; }

		public void SetTheCar(Car car)
		{
			if (car == null || car.IsTransient())
			{
				throw new ArgumentException("Cannot associate transient or null car");
			}

			CarId = car.Id;
			Car = car;
		}

		public void SetTheCarReference(int carId)
		{
			if (carId > 0)
			{
				CarId = carId;

				Car = null;
			}
		}

		private HashSet<Booking> bookings;
		public virtual ICollection<Booking> Bookings
		{
			get
			{
				return bookings ?? (bookings = new HashSet<Booking>());
			}
			set
			{
				bookings = new HashSet<Booking>(value);
			}
		}
	}
}
