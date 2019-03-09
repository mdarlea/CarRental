namespace CarRental.Identity.Data.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class DriverLicense
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(150)]
		public string DriverLicenseNumber { get; set; }

		[Required]
		[MaxLength(150)]
		public string CountryOfIssue { get; set; }

		[MaxLength(150)]
		public string StateOfIssue { get; set; }
	}
}
