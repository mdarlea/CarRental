namespace CarRental.Api.Models
{
	using FluentValidation;

	public class UserModel
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class UserValidator : AbstractValidator<UserModel>
	{
		public UserValidator()
		{
			RuleFor(u => u.Email).NotNull().NotEmpty().WithMessage("Email address is required");
			RuleFor(u => u.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
			RuleFor(u => u.LastName).NotNull().NotEmpty().WithMessage("Last name is required");			
		}
	}
}
