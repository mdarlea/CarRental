namespace CarRental.Api.Models
{
	using FluentValidation;

	public class ApplicationUserModel
	{	
		public UserModel User { get; set; }
		public DriverLicenseModel DriverLicense { get; set; }
		public CreditCardModel CreditCard { get; set; }
		public AddressModel BillingAddress { get; set; }

	}

	public class ApplicationUserValidator<T> : AbstractValidator<T>
		where T: ApplicationUserModel
	{
		public ApplicationUserValidator() {			
			RuleFor(u => u.User).NotNull().WithMessage("User information is required");
			RuleFor(u => u.DriverLicense).NotNull().WithMessage("Driver license is required");
			RuleFor(u => u.CreditCard).NotNull().WithMessage("Credit card is required");
			RuleFor(cc => cc.BillingAddress).NotNull().WithMessage("Billing address is required");
		}
	}
}
