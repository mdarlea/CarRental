namespace CarRental.Domain.Aggregates.BookingAgg
{
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using CarRental.Domain.Aggregates.UserAgg;
	using Swaksoft.Domain.Seedwork.Aggregates;
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Booking: Entity
	{
		public int AvailableCarId { get; private set; }
		public virtual AvailableCar AvailableCar { get; private set; }
		
		[Required]
		[MaxLength(250)]
		public string UserId { get; private set; }
		public User User { get; private set; }

		public DateTime From { get; set; }
		public DateTime To { get; set; }
		
		public void SetTheAvailableCarForThisBooking(AvailableCar availableCar)
		{
			if (availableCar == null || availableCar.IsTransient())
			{
				throw new ArgumentException("Cannot associate transient or null available car");
			}

			AvailableCarId = availableCar.Id;
			AvailableCar = availableCar;
		}

		public void SetTheAvailableCarReference(int availableCarId)
		{
			if (availableCarId > 0)
			{
				AvailableCarId = availableCarId;

				AvailableCar = null;
			}
		}

		public void SetTheUserForThisBooking(User user)
		{
			if (user == null || user.IsTransient())
			{
				throw new ArgumentException("Cannot associate transient or null user");
			}

			UserId = user.Id;
			User = user;
		}

		public void SetTheUserReference(string userId)
		{
			if (! string.IsNullOrWhiteSpace(userId))
			{
				UserId = userId;

				User = null;
			}
		}
	}
}
