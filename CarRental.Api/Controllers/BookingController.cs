namespace CarRental.Api.Controllers
{
	using System;	
	using System.Threading.Tasks;
	using CarRental.Application.Commands;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Swaksoft.Core.Dto;

	[Authorize(Policy = "ApiUser")]
	[Route("api/[controller]")]
	public class BookingController : ApiController
	{
		private readonly IMediator mediator;

		public BookingController(IMediator mediator) {
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpPost]
		public async Task<IActionResult> OnPost([FromBody]CreateBookingCommand command) {
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}
			if (!ModelState.IsValid) return BadRequest(ModelState);

			command.UserId = GetUserId();

			var result = await mediator.Send(command);

			return result.Status != ActionResultCode.Success
				? GetErrorResult(result)
				: Ok(result);
		}
	}
}