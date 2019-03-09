namespace CarRental.Api.Models
{
	using FluentValidation;

	public class CreateExternalApplicationUserModel : ApplicationUserModel
	{
		public string Provider { get; set; }
		public string ProviderKey { get; set; }
	}

	public class CreateExternalApplicationUserValidator : ApplicationUserValidator<CreateExternalApplicationUserModel>
	{
		public CreateExternalApplicationUserValidator()
		{
			RuleFor(e => e.Provider).NotNull().NotEmpty();
			RuleFor(e => e.ProviderKey).NotNull().NotEmpty();
		}
	}
}
