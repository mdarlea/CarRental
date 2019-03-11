namespace CarRental.Api.Models
{
	using FluentValidation;

	public class UpdateCreditCardModel
	{	
		public CreditCardModel CreditCard { get; set; }
		public AddressModel BillingAddress { get; set; }
	}

	public class UpdateCreditCardValidator : AbstractValidator<UpdateCreditCardModel>		
	{
		public UpdateCreditCardValidator() {			
			RuleFor(u => u.CreditCard).NotNull().WithMessage("Credit card is required");
			RuleFor(cc => cc.BillingAddress).NotNull().WithMessage("Billing address is required");
		}
	}
}
