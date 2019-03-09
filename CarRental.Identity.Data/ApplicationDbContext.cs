namespace CarRental.Identity.Data
{
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
			: base(option)
		{
		}

		public DbSet<DriverLicense> DriverLicenses { get; set; }

		public DbSet<CreditCard> CreditCards { get; set; }

		public DbSet<Address> Addresses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

			// Shorten key length for Identity 			
			modelBuilder.Entity<IdentityRole>(entity => {
				entity.Property(m => m.Id).HasMaxLength(250);
				entity.Property(m => m.Name).HasMaxLength(250);
				entity.Property(m => m.NormalizedName).HasMaxLength(250);
			});
			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{				
				entity.Property(m => m.LoginProvider).HasMaxLength(250);
				entity.Property(m => m.ProviderKey).HasMaxLength(250);
			});
			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(250);
				entity.Property(m => m.RoleId).HasMaxLength(250);
			});
			modelBuilder.Entity<IdentityUserToken<string>>(entity =>
			{			
				entity.Property(m => m.UserId).HasMaxLength(250);
				entity.Property(m => m.LoginProvider).HasMaxLength(250);
				entity.Property(m => m.Name).HasMaxLength(250);
			});
		}
    }
}