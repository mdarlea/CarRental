using CarRental.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace CarRental.Identity.Data.Mapping
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(m => m.Id).HasMaxLength(250);
			builder.Property(m => m.Email).HasMaxLength(250);
			builder.Property(m => m.NormalizedEmail).HasMaxLength(250);
			builder.Property(m => m.NormalizedUserName).HasMaxLength(250);
			builder.Property(m => m.UserName).HasMaxLength(250);

			builder.Property(m => m.TwoFactorEnabled).HasConversion(new BoolToZeroOneConverter<Int16>());
			builder.Property(m => m.PhoneNumberConfirmed).HasConversion(new BoolToZeroOneConverter<Int16>());
			builder.Property(m => m.EmailConfirmed).HasConversion(new BoolToZeroOneConverter<Int16>());
			builder.Property(m => m.LockoutEnabled).HasConversion(new BoolToZeroOneConverter<Int16>());

			builder.HasOne(au => au.DriverLicense).WithOne().HasForeignKey<ApplicationUser>(au => au.DriverLicenseId);
			builder.HasOne(au => au.CreditCard).WithOne().HasForeignKey<ApplicationUser>(au => au.CreditCardId);
		}
	}
}
