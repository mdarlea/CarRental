namespace CarRental.Data.UnitOfWork
{
	using CarRental.Data.UnitOfWork.Mapping;
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork;

	public class CarRentalUnitOfWork : EntityFrameworkUnitOfWork
	{
		public CarRentalUnitOfWork(DbContextOptions option, IMediator domainEventsDispatcher) 
			: base(option, domainEventsDispatcher)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new CarTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CarConfiguration());
			modelBuilder.ApplyConfiguration(new AvailableCarConfiguration());
		}
	}
}
