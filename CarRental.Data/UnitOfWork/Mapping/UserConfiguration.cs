namespace CarRental.Data.UnitOfWork.Mapping
{
	using CarRental.Domain.Aggregates.UserAgg;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.OwnsOne(u => u.Name);
		}
	}
}
