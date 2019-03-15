namespace CarRental.Application.Commands
{
	using FluentValidation;
	using MediatR;
	using Swaksoft.Core.Dto;
	using System;
	
	public class CreateBookingCommand : IRequest<ActionResult>
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public string UserId { get; set; }
		public int AvailableCarId { get; set; }
	}

	public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
	{
		public CreateBookingValidator() {
			RuleFor(e => e.From).NotEmpty().WithMessage("Start time is required")
			.GreaterThan(DateTime.Now).WithMessage("Start time must be in the future");

			RuleFor(e => e.To).NotEmpty().WithMessage("End time is required")
				.GreaterThan(DateTime.Now).WithMessage("End time must be in the future")
				.GreaterThan(e => e.From).WithMessage("End time must be after the Start time");
			
			RuleFor(e => e.AvailableCarId).GreaterThan(0);
		}
	}

}
