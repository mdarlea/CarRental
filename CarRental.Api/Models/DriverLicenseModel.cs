namespace CarRental.Api.Models
{
	using FluentValidation;

	public class DriverLicenseModel
	{		
		public string DriverLicenseNumber { get; set; }	
		public string CountryOfIssue { get; set; }		
		public string StateOfIssue { get; set; }
	}

	public class DriverLicenseValidator : AbstractValidator<DriverLicenseModel> {
		public DriverLicenseValidator() {
			RuleFor(d => d.DriverLicenseNumber).NotNull().NotEmpty().WithMessage("Driver license number is required");
			RuleFor(d => d.CountryOfIssue).NotNull().NotEmpty().WithMessage("Country of issue is required");
		}
	}
}
