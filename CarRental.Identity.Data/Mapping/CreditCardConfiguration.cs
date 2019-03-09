using CarRental.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Identity.Data.Mapping
{
	public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CreditCard> builder)
		{
			builder.HasOne(cc => cc.BillingAddress).WithOne().HasForeignKey<CreditCard>(cc => cc.BillingAddressId);
			builder.Property(cc => cc.Type).HasConversion<string>();
			builder.Property(cc => cc.Type).HasMaxLength(25);
		}
	}
}
