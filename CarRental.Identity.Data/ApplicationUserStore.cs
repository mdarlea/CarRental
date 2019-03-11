namespace CarRental.Identity.Data
{
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
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

		public async Task<IdentityResult> UpdateDriverLicenseAsync(ApplicationUser user, DriverLicense driverLicense, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();
			
			if (driverLicense == null)
			{
				throw new System.ArgumentNullException(nameof(driverLicense));
			}

			if (user == null)
			{
				throw new System.ArgumentNullException(nameof(user));
			}

			if (user.DriverLicense == null)
			{
				throw new System.ArgumentNullException(nameof(ApplicationUser.DriverLicense));
			}

			if (user.DriverLicense.Id != driverLicense.Id) {
				var error = new IdentityError
				{
					Description = $"Invalid driver license id = { driverLicense.Id }"
				};
				return IdentityResult.Failed(error);
			}

			// merge changes
			Context.Entry(user.DriverLicense).CurrentValues.SetValues(driverLicense);

			try
			{
				await SaveChanges(cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{				
				return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
			}

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> UpdateCreditCardAsync(ApplicationUser user, CreditCard creditCard, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (creditCard == null)
			{
				throw new System.ArgumentNullException(nameof(creditCard));
			}

			if (user == null)
			{
				throw new System.ArgumentNullException(nameof(user));
			}

			if (user.CreditCard == null)
			{
				throw new System.ArgumentNullException(nameof(ApplicationUser.CreditCard));
			}

			if (user.CreditCard.Id != creditCard.Id)
			{
				var error = new IdentityError
				{
					Description = $"Invalid credit card id = { creditCard.Id }"
				};
				return IdentityResult.Failed(error);
			}

			// merge changes
			Context.Entry(user.CreditCard).CurrentValues.SetValues(creditCard);

			try
			{
				await SaveChanges(cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{
				return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
			}

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> UpdateBillingAddressAsync(ApplicationUser user, Address billingAddress, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (billingAddress == null)
			{
				throw new System.ArgumentNullException(nameof(billingAddress));
			}

			if (user == null)
			{
				throw new System.ArgumentNullException(nameof(user));
			}

			if (user.CreditCard == null || user.CreditCard.BillingAddress == null)
			{
				throw new System.ArgumentNullException(nameof(ApplicationUser.CreditCard));
			}

			if (user.CreditCard.BillingAddress.Id != billingAddress.Id)
			{
				var error = new IdentityError
				{
					Description = $"Invalid billing address id = { billingAddress.Id }"
				};
				return IdentityResult.Failed(error);
			}

			// merge changes
			Context.Entry(user.CreditCard.BillingAddress).CurrentValues.SetValues(billingAddress);

			try
			{
				await SaveChanges(cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{
				return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
			}

			return IdentityResult.Success;
		}

		public virtual Task<ApplicationUser> FindUserWithDriverLicenseAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new System.ArgumentNullException(nameof(userId));
			}
			
			return Users.Include(u => u.DriverLicense).SingleOrDefaultAsync(u => u.Id.Equals(ConvertIdFromString(userId)), cancellationToken);
		}

		public virtual Task<ApplicationUser> FindUserWithCreditCardAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new System.ArgumentNullException(nameof(userId));
			}

			return Users.Include(u => u.CreditCard)
				.ThenInclude(cc => cc.BillingAddress)
				.SingleOrDefaultAsync(u => u.Id.Equals(ConvertIdFromString(userId)), cancellationToken);
		}

		public virtual Task<ApplicationUser> FindUserWithDriverLicenseAndCreditCardAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			ThrowIfDisposed();

			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new System.ArgumentNullException(nameof(userId));
			}

			return Users.Include(u => u.DriverLicense)
				.Include(u => u.CreditCard)
				.ThenInclude(cc => cc.BillingAddress)
				.SingleOrDefaultAsync(u => u.Id.Equals(ConvertIdFromString(userId)), cancellationToken);
		}
	}
}
