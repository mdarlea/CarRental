namespace CarRental.Api.Controllers
{
	using System;	
	using System.Threading.Tasks;
	using AutoMapper;
	using CarRental.Api.Authentication;
	using CarRental.Api.Models;
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Authorization;	
	using Microsoft.AspNetCore.Mvc;

	[Authorize(Policy = "ApiUser")]
	[Route("api/[controller]/[action]")]    
    public class UserController : ApiController
    {
		private readonly ApplicationUserManager userManager;
		private readonly IMapper mapper;

		public UserController(ApplicationUserManager userManager, IMapper mapper) {
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUserInformation([FromBody]UserModel userModel)
		{
			if (userModel == null)
			{
				throw new ArgumentNullException(nameof(userModel));
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userId = GetUserId();

			// get the user
			var identityUser = await userManager.FindByIdAsync(userId);
			if (identityUser == null)
			{
				return BadRequest();
			}

			// creates the updates user object
			var user = mapper.Map<ApplicationUser>(userModel);
			user.Id = userId;

			var result = await userManager.UpdateAsync(user);

			return (result.Succeeded) ? NoContent() : GetErrorResult(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDriverLicense([FromBody]DriverLicenseModel driverLicenseModel) {
			if (driverLicenseModel == null)
			{
				throw new ArgumentNullException(nameof(driverLicenseModel));
			}
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var userId = GetUserId();

			// get the user
			var user = await userManager.FindUserWithDriverLicenseAsync(userId);
			if (user == null) {
				return BadRequest();
			}

			var driverLicense = mapper.Map<DriverLicense>(driverLicenseModel);

			var result = await userManager.UpdateDriverLicenseAsync(user, driverLicense);

			return (result.Succeeded) ? NoContent() : GetErrorResult(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCreditCard([FromBody]UpdateCreditCardModel creditCardModel)
		{
			if (creditCardModel == null)
			{
				throw new ArgumentNullException(nameof(creditCardModel));
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userId = GetUserId();

			// get the user
			var user = await userManager.FindUserWithDriverLicenseAsync(userId);
			if (user == null)
			{
				return BadRequest();
			}

			var creditCard = mapper.Map<CreditCard>(creditCardModel.CreditCard);
			var billingAddress = mapper.Map<Address>(creditCardModel.CreditCard);

			var result = await userManager.UpdateCreditCardAsync(user, creditCard);
			if (!result.Succeeded) {
				return GetErrorResult(result);
			}

			result = await userManager.UpdateBillingAddressAsync(user, billingAddress);

			return (result.Succeeded) ? NoContent() : GetErrorResult(result);
		}
	}
}