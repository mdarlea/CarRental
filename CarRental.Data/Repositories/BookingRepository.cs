namespace CarRental.Data.Repositories
{
	using CarRental.Data.UnitOfWork;
	using CarRental.Domain.Aggregates.BookingAgg;
	using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

	public class BookingRepository : Repository<Booking>, IBookingRepository
	{
		public BookingRepository(CarRentalUnitOfWork unitOfWork) 
			: base(unitOfWork)
		{
		}
	}
}
