namespace CarRental.Api.Tests
{
	using CarRental.Api.Infrastructure.Repositories;
	using CarRental.Data.Repositories;
	using CarRental.Data.UnitOfWork;
	using CarRental.Data.UnitOfWork.Mapping;
	using CarRental.Domain.Aggregates.BookingAgg;
	using CarRental.Domain.Aggregates.CarAgg;
	using CarRental.Domain.Aggregates.CarTypeAgg;
	using CarRental.Domain.Aggregates.UserAgg;
	using CarRental.Identity.Data.Tests;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[TestClass]
	public class AvailableCarRepositoryTests
	{
		[TestMethod]
		public async Task GetAvailableCarsTest()
		{
			using (var factory = new CarRentalUnitOfWorkFactory()) {
				using (var context = factory.CreateContext(false)) {
					SeedData(context);
				}
				using (var context = factory.CreateContext(true))
				{
					var repository = new Infrastructure.Repositories.AvailableCarRepository(context);

					DateTime from = DateTime.Now;
					from = from.AddDays(1);
					DateTime to = DateTime.Now;
					to = to.AddDays(6);

					var data = await repository.GetAvailableCarsAsync(from, to);
					var availableCar = data.SingleOrDefault(d => d.Id == 1);
					Assert.IsNotNull(availableCar);
					Assert.IsTrue(availableCar.AvailableCars == 8);
				}
			}
		}

		private void SeedData(CarRentalUnitOfWork context) {
			var user = new User
			{
				Name = new Name("Testor", "User"),
				Email = "testor@test.com"
			};
			user.ChangeCurrentIdentity(Guid.NewGuid().ToString());

			context.Set<User>().Add(user);
			context.SaveChanges();

			DateTime from = DateTime.Now;
			from = from.AddDays(2);
			DateTime to = DateTime.Now;
			to = to.AddDays(5);

			var booking = new Booking() {
				From = from,
				To = to
			};
			booking.SetTheAvailableCarReference(1);
			booking.SetTheUserForThisBooking(user);
			context.Bookings.Add(booking);
			
			from = DateTime.Now;
			from = from.AddDays(3);
			to = DateTime.Now;
			to = to.AddDays(4);

			booking = new Booking()
			{
				From = from,
				To = to
			};
			booking.SetTheAvailableCarReference(1);
			booking.SetTheUserForThisBooking(user);
			context.Bookings.Add(booking);

			context.SaveChanges();
		}
	}
}
