namespace CarRental.Domain.Aggregates.BookingAgg
{
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using CarRental.Domain.Aggregates.UserAgg;
	using System;

	public static class BookingFactory
	{
		public static Booking CreateBooking(AvailableCar availableCar, User user, DateTime from, DateTime to) {
			if (availableCar == null)
			{
				throw new ArgumentNullException(nameof(availableCar));
			}

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var booking = new Booking
			{
				From = from,
				To = to
			};
			booking.SetTheUserForThisBooking(user);
			booking.SetTheAvailableCarForThisBooking(availableCar);

			return booking;
		}
	}
}
