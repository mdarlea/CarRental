namespace CarRental.Api.Authentication
{
	using CarRental.Identity.Data;
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Threading.Tasks;

	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		private readonly IUserStore<ApplicationUser> store;

		public ApplicationUserManager(
			IUserStore<ApplicationUser> store, 
			IOptions<IdentityOptions> optionsAccessor, 
			IPasswordHasher<ApplicationUser> passwordHasher, 
			IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
			IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
			ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
			IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : 
			base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			this.store = store ?? throw new ArgumentNullException(nameof(store));
		}

		private ApplicationUserStore ApplicationUserStore
		{
			get
			{
				return store as ApplicationUserStore;
			}
		}

		public async Task<IdentityResult> UpdateDriverLicenseAsync(ApplicationUser user, DriverLicense driverLicense) {
			ThrowIfDisposed();
			return await ApplicationUserStore.UpdateDriverLicenseAsync(user, driverLicense, CancellationToken);					   			
		}

		public async Task<IdentityResult> UpdateCreditCardAsync(ApplicationUser user, CreditCard creditCard)
		{
			ThrowIfDisposed();
			return await ApplicationUserStore.UpdateCreditCardAsync(user, creditCard, CancellationToken);
		}

		public async Task<IdentityResult> UpdateBillingAddressAsync(ApplicationUser user, Address billingAddress)
		{
			ThrowIfDisposed();
			return await ApplicationUserStore.UpdateBillingAddressAsync(user, billingAddress, CancellationToken);
		}

		public async Task<ApplicationUser> FindUserWithDriverLicenseAsync(string userId) {
			ThrowIfDisposed();
			if (userId == null)
			{
				throw new ArgumentNullException(nameof(userId));
			}
			return await ApplicationUserStore.FindUserWithDriverLicenseAsync(userId, CancellationToken);
		}

		public async Task<ApplicationUser> FindUserWithCreditCardAsync(string userId)
		{
			ThrowIfDisposed();
			if (userId == null)
			{
				throw new ArgumentNullException(nameof(userId));
			}
			return await ApplicationUserStore.FindUserWithCreditCardAsync(userId, CancellationToken);
		}

		public async Task<ApplicationUser> FindUserWithCreditCardAndDriverLicenseAsync(string userId)
		{
			ThrowIfDisposed();
			if (userId == null)
			{
				throw new ArgumentNullException(nameof(userId));
			}
			return await ApplicationUserStore.FindUserWithDriverLicenseAndCreditCardAsync(userId, CancellationToken);
		}

		public async Task<IdentityResult> CreateAsync(ApplicationUser user, DriverLicense driverLicense, Address billingAddress, CreditCard creditCard, string password)
		{
			ApplicationUserManager userManager = this;
			userManager.ThrowIfDisposed();
			
			if (user == null)
				throw new ArgumentNullException("user");
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentNullException("password");
			if (driverLicense == null)
			{
				throw new ArgumentNullException(nameof(driverLicense));
			}

			if (billingAddress == null)
			{
				throw new ArgumentNullException(nameof(billingAddress));
			}

			if (creditCard == null)
			{
				throw new ArgumentNullException(nameof(creditCard));
			}

			IdentityResult identityResult = await userManager.UpdatePasswordHash(user, password, true);
			if (!identityResult.Succeeded)
				return identityResult;
			return await userManager.CreateAsync(user, driverLicense,billingAddress, creditCard);
		}

		public async Task<IdentityResult> CreateAsync(ApplicationUser user, DriverLicense driverLicense, Address billingAddress, CreditCard creditCard) {
			ThrowIfDisposed();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			if (driverLicense == null)
			{
				throw new ArgumentNullException(nameof(driverLicense));
			}

			if (billingAddress == null)
			{
				throw new ArgumentNullException(nameof(billingAddress));
			}

			if (creditCard == null)
			{
				throw new ArgumentNullException(nameof(creditCard));
			}
	
			// validates the user
			var validationContext = new ValidationContext(user);
			Validator.ValidateObject(user, validationContext, validateAllProperties: true);

			// validates the billing address
			validationContext = new ValidationContext(billingAddress);
			Validator.ValidateObject(billingAddress, validationContext, validateAllProperties: true);

			// validates the driver license
			validationContext = new ValidationContext(driverLicense);
			Validator.ValidateObject(driverLicense, validationContext, validateAllProperties: true);

			// validates the credit card 
			validationContext = new ValidationContext(creditCard);
			Validator.ValidateObject(creditCard, validationContext, validateAllProperties: true);

			ApplicationUserManager userManager = this;			
			await userManager.UpdateSecurityStampInternal(user);
			IdentityResult identityResult = await userManager.ValidateUserAsync(user);
			if (!identityResult.Succeeded)
				return identityResult;
			if (userManager.Options.Lockout.AllowedForNewUsers && userManager.SupportsUserLockout)
				await userManager.GetUserLockoutStore().SetLockoutEnabledAsync(user, true, userManager.CancellationToken);
			await userManager.UpdateNormalizedUserNameAsync(user);
			await userManager.UpdateNormalizedEmailAsync(user);

			return await userManager.ApplicationUserStore.CreateAsync(user, driverLicense, billingAddress, creditCard, userManager.CancellationToken);
		}

		private async Task UpdateSecurityStampInternal(ApplicationUser user)
		{
			ApplicationUserManager userManager = this;
			if (!userManager.SupportsUserSecurityStamp)
				return;
			await userManager.GetSecurityStore().SetSecurityStampAsync(user, userManager.GenerateNewAuthenticatorKey(), userManager.CancellationToken);
		}

		private IUserSecurityStampStore<ApplicationUser> GetSecurityStore()
		{
			IUserSecurityStampStore<ApplicationUser> store = Store as IUserSecurityStampStore<ApplicationUser>;
			if (store != null)
				return store;
			throw new NotSupportedException("User store is not of type user security stamp store");
		}

		private IUserLockoutStore<ApplicationUser> GetUserLockoutStore()
		{
			IUserLockoutStore<ApplicationUser> store = Store as IUserLockoutStore<ApplicationUser>;
			if (store != null)
				return store;
			throw new NotSupportedException("User store is not of type user lockout store");
		}		
	}
}
