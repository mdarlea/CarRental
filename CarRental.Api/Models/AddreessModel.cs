namespace CarRental.Api.Models
{
	using FluentValidation;
	
	public class AddressModel 
    {
		public int Id { get; set; }
        public string StreetAddress { get; set; }        
        public string SuiteNumber { get; set; }        
        public int GeolocationStreetNumber { get; set; }        
        public string GeolocationStreet { get; set; }
        public string City { get; set; }       
        public string State { get; set; }        
        public string Zip { get; set; }        
        public string CountryIsoCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

	public class AddressValidator : AbstractValidator<AddressModel>
	{
		public AddressValidator() {
			RuleFor(a => a.StreetAddress).NotNull().NotEmpty().WithMessage("Street address is required");
			RuleFor(a => a.City).NotNull().NotEmpty().WithMessage("City is required");
			RuleFor(a => a.CountryIsoCode).NotNull().NotEmpty().WithMessage("Country ISO code is required");
			RuleFor(a => a.Latitude).NotEqual(0).WithMessage("Latitude is required");
			RuleFor(a => a.Longitude).NotEqual(0).WithMessage("Longitude is required");
		}
	}
}
