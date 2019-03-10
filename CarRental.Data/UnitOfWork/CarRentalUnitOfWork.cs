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

	public class CarRentalUnitOfWorkDesignFactory : IDesignTimeDbContextFactory<CarRentalUnitOfWork>
	{
		public CarRentalUnitOfWork CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CarRentalUnitOfWork>()
			   .UseMySQL("Data Source=mysql502.discountasp.net; port=3306; Initial Catalog=MYSQL5_948078_carrental; uid=carrentaluser; pwd=demo2019;");

			return new CarRentalUnitOfWork(optionsBuilder.Options, new NoMediator());
		}

		class NoMediator : IMediator
		{
			public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
			{
				return Task.CompletedTask;
			}

			public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.CompletedTask;
			}

			public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.FromResult<TResponse>(default(TResponse));
			}

			public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
			{
				return Task.CompletedTask;
			}
		}
	}
}
