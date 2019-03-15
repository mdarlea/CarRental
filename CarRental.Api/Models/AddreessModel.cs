namespace CarRental.Api.Models
{
	using FluentValidation;
	
	public class AddressModel 
    {
		public int? Id { get; set; }
        public string StreetAddress { get; set; }        
        public string SuiteNumber { get; set; }         
        public string City { get; set; }       
        public string State { get; set; }        
        public string Zip { get; set; }        
    }

	public class AddressValidator : AbstractValidator<AddressModel>
	{
		public AddressValidator() {
			RuleFor(a => a.StreetAddress).NotNull().NotEmpty().WithMessage("Street address is required");
			RuleFor(a => a.City).NotNull().NotEmpty().WithMessage("City is required");			
		}
	}
}
