namespace CarRental.Api.Models
{	
	using FluentValidation;
	using System;
	
	public class CreditCardModel
	{
		public enum CreditCardType {
			VISA,
			MasterCard,
			AmericanExpress
		}
				
		public CreditCardType Type { get; set; }		
		public string CreditCardNumber { get; set; }
		public string NameOnCard { get; set; }		
		public DateTime ExpirationTime { get; set; }		
	}

	public class CreditCardValidator : AbstractValidator<CreditCardModel> {
		public CreditCardValidator() {
			RuleFor(cc => cc.CreditCardNumber).NotNull().NotEmpty().WithMessage("Credit Card number is required");
			RuleFor(cc => cc.NameOnCard).NotNull().NotEmpty().WithMessage("Name on card is required");
			RuleFor(cc => cc.ExpirationTime).NotEqual(DateTime.MinValue).WithMessage("Credit card expiration date is required");			
		}
	}
}
