namespace CarRental.Api.Models
{
	using FluentValidation;
	using System;
	
	public class FindAvailableCarsModel
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}

	public class FindAvailableCarsValidator : AbstractValidator<FindAvailableCarsModel>
	{
		public FindAvailableCarsValidator() {
			var now = DateTime.Now;
			
			RuleFor(e => e.From).NotEmpty().WithMessage("From time is required")
				.GreaterThanOrEqualTo(now).WithMessage("From time must be in the future");

			RuleFor(e => e.To).NotEmpty().WithMessage("To time is required")
				.GreaterThanOrEqualTo(now).WithMessage("To time must be in the future")
				.GreaterThan(e => e.From).WithMessage("To time must be after the From time");
		}
	}
}
