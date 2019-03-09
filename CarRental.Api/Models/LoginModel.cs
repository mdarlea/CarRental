namespace CarRental.Api.Models
{
	using FluentValidation;
	using System.ComponentModel.DataAnnotations;

	public class LoginModel
	{		
		public string UserName { get; set; }
				
		[DataType(DataType.Password)]		
		public string Password { get; set; }		
	}

	public class LoginValidator : AbstractValidator<LoginModel> {
		public LoginValidator() {
			RuleFor(l => l.UserName).NotNull().NotEmpty().WithMessage("User name is required");
			RuleFor(l => l.Password).NotNull().NotEmpty().WithMessage("Password is required");
		}
	}
}
