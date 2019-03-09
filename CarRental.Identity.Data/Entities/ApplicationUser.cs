namespace CarRental.Identity.Data.Entities
{
	using CarRental.Identity.Data.ValueObjects;
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations;

	public class ApplicationUser : IdentityUser
    {
        [Required]
        public Name Name { get; set; }		

		public int DriverLicenseId { get; set; }
		public virtual DriverLicense DriverLicense { get; set; }

		public int CreditCardId { get; set; }
		public virtual CreditCard CreditCard { get; set; }

    }
}