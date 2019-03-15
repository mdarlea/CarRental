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
		private ApplicationUser user;
		private Address billingAddress;
		private CreditCard creditCard;
		private DriverLicense driverLicense;

		[TestMethod]
		public async Task CreateInvalidUser()
		{
			StubData();

			// user to create
			user = new ApplicationUser
			{
				Email = "testor@gmail.com",
				UserName = "testor@gmail.com"		
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
			StubData();

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
				}
			}
		}

		[TestMethod]
		public async Task UpdateDriverLicense()
		{
			StubData();

			using (var factory = new ApplicationDbContextFactory())
			{
				using (var dbContext = factory.CreateContext(false))
				{
					using (var userStore = new ApplicationUserStore(dbContext))
					{
						var result = await userStore.CreateAsync(user, this.driverLicense, billingAddress, creditCard);

						Assert.IsTrue(result.Succeeded);
					}
				}

				DriverLicense driverLicense;
				using (var dbContext = factory.CreateContext(true))
				{					
					using (var userStore = new ApplicationUserStore(dbContext))
					{
						var applicationUser = await userStore.FindUserWithDriverLicenseAsync(user.Id);

						Assert.IsNotNull(applicationUser);

						driverLicense = new DriverLicense
						{
							Id = applicationUser.DriverLicenseId,
							DriverLicenseNumber = "70001",
							CountryOfIssue = this.driverLicense.CountryOfIssue
						};

						var result = await userStore.UpdateDriverLicenseAsync(applicationUser, driverLicense);

						Assert.IsTrue(result.Succeeded);
					}
				}

				using (var dbContext = factory.CreateContext(false))
				{
					var applicationUser = await dbContext.Users
												   .Include(u => u.DriverLicense)												   
												   .SingleOrDefaultAsync(u => u.UserName == user.UserName);
					
					Assert.IsNotNull(applicationUser.DriverLicense);
					Assert.IsTrue(applicationUser.DriverLicenseId == applicationUser.DriverLicense.Id);
					Assert.IsTrue(applicationUser.DriverLicense.DriverLicenseNumber == driverLicense.DriverLicenseNumber);
					Assert.IsTrue(applicationUser.DriverLicense.CountryOfIssue == driverLicense.CountryOfIssue);					
				}
			}
		}

		private void StubData()
		{
			// user to create
			user = new ApplicationUser
			{
				Email = "testor@gmail.com",
				UserName = "testor@gmail.com",
				Name = new Name("Testor", "User")
			};
			billingAddress = new Address
			{
				StreetAddress = "Str. Brasov nr.9",
				SuiteNumber = "3",
				City = "Timisoara"
			};
			creditCard = new CreditCard
			{
				Type = CreditCard.CreditCardType.VISA,
				CreditCardNumber = "1234",
				NameOnCard = "Testor User",

			};
			driverLicense = new DriverLicense
			{
				DriverLicenseNumber = "567891",
				CountryOfIssue = "Romania"
			};
		}
	}
}
