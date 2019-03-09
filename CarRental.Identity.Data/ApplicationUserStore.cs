namespace CarRental.Identity.Data
{
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;

	public class ApplicationUserStore
		: UserStore<ApplicationUser,
					IdentityRole,
					ApplicationDbContext,
					string,
					IdentityUserClaim<string>,
					IdentityUserRole<string>,
					IdentityUserLogin<string>,
					IdentityUserToken<string>,
					IdentityRoleClaim<string>>
	{
		private readonly ApplicationDbContext context;

		public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) 
			: base(context, describer)
		{
			this.context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		public async Task<IdentityResult> CreateAsync(
			ApplicationUser user, 
			DriverLicense driverLicense, 
			Address billingAddress, 
			CreditCard creditCard,
			CancellationToken cancellationToken = default(CancellationToken)) {
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (user == null)
			{
				throw new System.ArgumentNullException(nameof(user));
			}

			if (driverLicense == null)
			{
				throw new System.ArgumentNullException(nameof(driverLicense));
			}

			if (billingAddress == null)
			{
				throw new System.ArgumentNullException(nameof(billingAddress));
			}

			if (creditCard == null)
			{
				throw new System.ArgumentNullException(nameof(creditCard));
			}

			IdentityResult result = new IdentityResult();
			using (var transaction = await context.Database.BeginTransactionAsync())
			{
				try
				{
					// adds the billing address
					context.Addresses.Add(billingAddress);					

					await context.SaveChangesAsync();

					creditCard.BillingAddressId = billingAddress.Id;
					creditCard.BillingAddress = billingAddress;

					// adds the driver license and the credit card
					context.DriverLicenses.Add(driverLicense);
					context.CreditCards.Add(creditCard);
					await context.SaveChangesAsync();

					// creates the user
					user.DriverLicenseId = driverLicense.Id;
					user.DriverLicense = driverLicense;

					user.CreditCardId = creditCard.Id;
					user.CreditCard = creditCard;

					result = await base.CreateAsync(user, cancellationToken);

					// rolls back the transaction if user could not be created
					if (!result.Succeeded)
					{
						transaction.Rollback();
						return result;
					}

					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
				
			}
						

			return result;			
		}
	}
}
