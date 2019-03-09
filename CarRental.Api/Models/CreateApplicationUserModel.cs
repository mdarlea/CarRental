namespace CarRental.Api.Models
{
	using FluentValidation;
	using System.ComponentModel.DataAnnotations;

	public class CreateApplicationUserModel : ApplicationUserModel
	{	
		[DataType(DataType.Password)]	
		public string Password { get; set; }
	}

	public class CreateApplicationUserValidator : ApplicationUserValidator<CreateApplicationUserModel> {
		public CreateApplicationUserValidator() {			
			RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Password is required");
			RuleFor(u => u.Password).MinimumLength(6).MaximumLength(100).WithMessage("The password must be at least 6 characters long");			
		}
	}
}
