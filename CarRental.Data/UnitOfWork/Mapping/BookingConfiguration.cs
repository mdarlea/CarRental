namespace CarRental.Data.UnitOfWork.Mapping
{
	using CarRental.Domain.Aggregates.BookingAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class BookingConfiguration : IEntityTypeConfiguration<Booking>
	{
		public void Configure(EntityTypeBuilder<Booking> builder)
		{
			builder.HasOne(b => b.User).WithMany().HasForeignKey(b => b.UserId);
			builder.HasOne(b => b.AvailableCar).WithMany().HasForeignKey(b => b.AvailableCarId);
		}
	}
}
