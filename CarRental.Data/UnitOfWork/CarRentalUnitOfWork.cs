namespace CarRental.Data.UnitOfWork
{
	using MediatR;
	using CarRental.Data.UnitOfWork.Mapping;
	using CarRental.Domain.Aggregates.AvailableCarAgg;
	using CarRental.Domain.Aggregates.CarAgg;
	using CarRental.Domain.Aggregates.CarTypeAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;
	using Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork;
	using System.Threading.Tasks;
	using System.Threading;
	using CarRental.Domain.Aggregates.BookingAgg;

	public class CarRentalUnitOfWork : EntityFrameworkUnitOfWork
	{
		public CarRentalUnitOfWork(DbContextOptions option, IMediator domainEventsDispatcher) 
			: base(option, domainEventsDispatcher)
		{
		}

		public DbSet<CarType> CarTypes { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<AvailableCar> AvailableCars { get; set; }
		public DbSet<Booking> Bookings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CarTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CarConfiguration());
			modelBuilder.ApplyConfiguration(new AvailableCarConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new BookingConfiguration());
		}
	}
}
