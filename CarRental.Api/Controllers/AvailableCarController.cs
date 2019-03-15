namespace CarRental.Api.Controllers
{
	using System;
	using System.Threading.Tasks;
	using CarRental.Api.Infrastructure.Services;
	using CarRental.Api.Models;
	using CarRental.Application.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Swaksoft.Core.Dto;

	[Authorize(Policy = "ApiUser")]
	[Route("api/[controller]")]
	public class AvailableCarController : ApiController
	{
		private readonly IAvailableCarService availableCarService;
		private readonly IAvailableCarAppService availableCarAppService;

		public AvailableCarController(IAvailableCarService availableCarService, IAvailableCarAppService availableCarAppService)
		{
			this.availableCarService = availableCarService ?? throw new ArgumentNullException(nameof(availableCarService));
			this.availableCarAppService = availableCarAppService ?? throw new ArgumentNullException(nameof(availableCarAppService));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> FindById(int id) {
			var result = await availableCarAppService.FindAvailableCarById(id);
			return result.Status != ActionResultCode.Success
			  ? GetErrorResult(result)
			  : Ok(result);
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