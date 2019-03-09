using CarRental.Identity.Data.Entities;
using CarRental.Identity.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CarRental.Identity.Data.Tests
{
	[TestClass]
	public class UserStoreTests
	{
		[TestMethod]
		public async Task CreateInvalidUser()
		{
			// user to create
			var user = new ApplicationUser
			{
				Email = "testor@gmail.com",
				UserName = "testor@gmail.com"		
			};
			var billingAddress = new Address
			{
				StreetAddress = "Str. Brasov nr.9",
				SuiteNumber = "3",
				City = "Timisoara",
				CountryIsoCode = "ro",
				Latitude = 45.747699,
				Longitude = 21.222093900000004
			};
			var creditCard = new CreditCard
			{
				Type = CreditCard.CreditCardType.VISA,
				CreditCardNumber = "1234",
				NameOnCard = "Testor User",

			};
			var driverLicense = new DriverLicense
			{
				DriverLicenseNumber = "567891",
				CountryOfIssue = "Romania"
			};

			using (var factory = new ApplicationDbContextFactory())
			{
				using (var dbContext = factory.CreateContext(false))
				{
					using (var userStore = new ApplicationUserStore(dbContext))
					{
						await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
						{
							await userStore.CreateAsync(user, driverLicense, billingAddress, creditCard);
						});
					}
				}
			}
		}

		[TestMethod]
		public async Task CreateNewUser()
		{
			// user to create
			var user = new ApplicationUser
			{
				Email = "testor@gmail.com",
				UserName = "testor@gmail.com",
				Name = new Name("Testor", "User")
			};
			var billingAddress = new Address
			{
				StreetAddress = "Str. Brasov nr.9",
				SuiteNumber = "3",
				City = "Timisoara",
				CountryIsoCode = "ro",
				Latitude = 45.747699,
				Longitude = 21.222093900000004
			};
			var creditCard = new CreditCard
			{
				Type = CreditCard.CreditCardType.VISA,
				CreditCardNumber = "1234",
				NameOnCard = "Testor User",

			};
			var driverLicense = new DriverLicense
			{
				DriverLicenseNumber = "567891",
				CountryOfIssue = "Romania"
			};

			using (var factory = new ApplicationDbContextFactory())
			{
				using (var dbContext = factory.CreateContext(true))
				{
					using (var userStore = new ApplicationUserStore(dbContext))
					{
						var result = await userStore.CreateAsync(user, driverLicense, billingAddress, creditCard);

						Assert.IsTrue(result.Succeeded);
					}
				}

				using (var dbContext = factory.CreateContext(false))
				{
					var applicationUser = await dbContext.Users
												   .Include(u => u.DriverLicense)
												   .Include(u => u.CreditCard).ThenInclude(cc => cc.BillingAddress)
												   .SingleOrDefaultAsync(u => u.UserName == user.UserName);
					Assert.IsNotNull(applicationUser);
					Assert.IsTrue(applicationUser.Email == user.Email);
					Assert.IsTrue(applicationUser.Name.Equals(user.Name));

					Assert.IsNotNull(applicationUser.DriverLicense);
					Assert.IsTrue(applicationUser.DriverLicenseId == applicationUser.DriverLicense.Id);
					Assert.IsTrue(applicationUser.DriverLicense.DriverLicenseNumber == driverLicense.DriverLicenseNumber);
					Assert.IsTrue(applicationUser.DriverLicense.CountryOfIssue == driverLicense.CountryOfIssue);

					Assert.IsNotNull(applicationUser.CreditCard);
					Assert.IsTrue(applicationUser.CreditCardId == applicationUser.CreditCard.Id);
					Assert.IsTrue(applicationUser.CreditCard.Type == creditCard.Type);
					Assert.IsTrue(applicationUser.CreditCard.CreditCardNumber == creditCard.CreditCardNumber);
					Assert.IsTrue(applicationUser.CreditCard.NameOnCard == creditCard.NameOnCard);

					Assert.IsNotNull(applicationUser.CreditCard.BillingAddress);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddressId == applicationUser.CreditCard.BillingAddress.Id);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.StreetAddress == billingAddress.StreetAddress);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.SuiteNumber == billingAddress.SuiteNumber);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.City == billingAddress.City);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.CountryIsoCode == billingAddress.CountryIsoCode);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.Latitude == billingAddress.Latitude);
					Assert.IsTrue(applicationUser.CreditCard.BillingAddress.Longitude == billingAddress.Longitude);
				}
			}
		}
	}
}
