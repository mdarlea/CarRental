namespace CarRental.Api.Controllers
{
	using System;
	using System.Threading.Tasks;
	using CarRental.Api.Infrastructure.Services;
	using CarRental.Api.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize(Policy = "ApiUser")]
	[Route("api/[controller]")]
	public class AvailableCarController : ApiController
	{
		private readonly IAvailableCarService availableCarService;

		public AvailableCarController(IAvailableCarService availableCarService)
		{
			this.availableCarService = availableCarService ?? throw new ArgumentNullException(nameof(availableCarService));
		}

		[HttpPost]
		public async Task<IActionResult> Find([FromBody]FindAvailableCarsModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var result = await availableCarService.FindAvailableCarsAsync(model.From, model.To);

			return Ok(result);
		}
	}
}