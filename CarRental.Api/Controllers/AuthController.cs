// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia.Host.Controllers
{
	using System;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AutoMapper;
	using CarRental.Api.Authentication;
	using CarRental.Api.Models;
	using CarRental.Identity.Data.Entities;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Options;
	using Newtonsoft.Json;
	using ExternalLoginInfo = CarRental.Api.Models.ExternalLoginInfo;

	[Route("api/[controller]/[action]")]
	public class AuthController : ApiController
	{		
		private readonly ApplicationUserManager userManager;		
		private readonly IMapper mapper;
		private readonly IJwtFactory jwtFactory;
		private readonly JwtIssuerOptions jwtOptions;

		public AuthController(			
			ApplicationUserManager userManager,			
			IMapper mapper,
			IJwtFactory jwtFactory,
			IOptions<JwtIssuerOptions> jwtOptions
			)
		{
			if (jwtOptions == null)
			{
				throw new ArgumentNullException(nameof(jwtOptions));
			}
	
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));			
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			this.jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
			this.jwtOptions = jwtOptions.Value;
		}
		
		[HttpPost]		
		public async Task<IActionResult> Login([FromBody]LoginModel viewModel) {
			if (viewModel == null)
			{
				throw new ArgumentNullException(nameof(viewModel));
			}
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var identity = await GetClaimsIdentity(viewModel.UserName, viewModel.Password);
			if (identity == null)
			{
				ModelState.TryAddModelError("login_failure", "Invalid username or password.");
				return BadRequest(ModelState);
			}

			var jwt = await Tokens.GenerateJwt(identity, jwtFactory, viewModel.UserName, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

			return new OkObjectResult(jwt);
		}
				
		[HttpPost]
		public async Task<IActionResult> LoginExternal([FromBody]ExternalLoginInfo externalLoginInfo)
		{
			if (externalLoginInfo == null 
				|| string.IsNullOrWhiteSpace(externalLoginInfo.Provider) 
				|| string.IsNullOrWhiteSpace(externalLoginInfo.ProviderKey)
				|| string.IsNullOrWhiteSpace(externalLoginInfo.AccessToken))
			{
				throw new ArgumentNullException(nameof(externalLoginInfo));
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (User.Identity.IsAuthenticated)
			{
				return BadRequest("User is already authorized");
			}

			// ToDo: validate the access token

			var user = await userManager.FindByLoginAsync(externalLoginInfo.Provider, externalLoginInfo.ProviderKey);

			var hasRegistered = user != null;

			if (hasRegistered)
			{
				// if user has already registered then issue a jwt token
				return await GenerateJwtTokenAsync(user);				
			}
					
			return new OkObjectResult(new ExternalLoginInfo() {
				Provider = externalLoginInfo.Provider,
				ProviderKey = externalLoginInfo.ProviderKey
			});
		}


		[HttpPost]
		public async Task<IActionResult> Register([FromBody]CreateApplicationUserModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// creates the new user
			var userIdentity = mapper.Map<ApplicationUser>(model.User);
			var billingAddress = mapper.Map<Address>(model.BillingAddress);
			var creditCard = mapper.Map<CreditCard>(model.CreditCard);
			var driverLicense = mapper.Map<DriverLicense>(model.DriverLicense);

			var result = await userManager.CreateAsync(userIdentity, driverLicense, billingAddress, creditCard, model.Password);

			if (!result.Succeeded) return GetErrorResult(result);

			// the user is registered, issue a jwt token
			return await GenerateJwtTokenAsync(userIdentity);
		}

		[HttpPost]
		public async Task<IActionResult> RegisterExternal([FromBody]CreateExternalApplicationUserModel model)
		{
			if (model == null || string.IsNullOrWhiteSpace(model.Provider) || string.IsNullOrWhiteSpace(model.ProviderKey))
			{
				throw new ArgumentNullException(nameof(model));
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// creates the new user			
			var userIdentity = mapper.Map<ApplicationUser>(model.User);
			var billingAddress = mapper.Map<Address>(model.BillingAddress);
			var creditCard = mapper.Map<CreditCard>(model.CreditCard);
			var driverLicense = mapper.Map<DriverLicense>(model.DriverLicense);

			var result = await userManager.CreateAsync(userIdentity, driverLicense, billingAddress, creditCard);

			if (!result.Succeeded) return GetErrorResult(result);
			
			// creates the enternal login
			var loginInfo = new UserLoginInfo(model.Provider, model.ProviderKey, model.Provider);
			var loginResult = await userManager.AddLoginAsync(userIdentity, loginInfo);

			// the user is registered, issue a jwt token
			return await GenerateJwtTokenAsync(userIdentity);
		}

		private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
				return await Task.FromResult<ClaimsIdentity>(null);

			// get the user to verifty
			var userToVerify = await userManager.FindByNameAsync(userName);

			if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

			// check the credentials
			if (await userManager.CheckPasswordAsync(userToVerify, password))
			{
				return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
			}

			// Credentials are invalid, or account doesn't exist
			return await Task.FromResult<ClaimsIdentity>(null);
		}

		private async Task<IActionResult> GenerateJwtTokenAsync(ApplicationUser userIdentity)
		{
			var identity = jwtFactory.GenerateClaimsIdentity(userIdentity.UserName, userIdentity.Id);
			var jwt = await Tokens.GenerateJwt(identity, jwtFactory, userIdentity.UserName, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
			return new OkObjectResult(jwt);
		}		
	}
}
