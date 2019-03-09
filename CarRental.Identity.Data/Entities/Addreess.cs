namespace CarRental.Identity.Data.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Address 
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
				
		[MaxLength(150)]
		[Required(ErrorMessage = "Street address is required")]
        public string StreetAddress { get; set; }

        [MaxLength(10)]
        public string SuiteNumber { get; set; }
        
        public int GeolocationStreetNumber { get; set; }

        [MaxLength(150)]
        public string GeolocationStreet { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [MaxLength(30)]
        public string State { get; set; }

        [MaxLength(20)]        
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country ISO code is required")]
        [MaxLength(50)]
        public string CountryIsoCode { get; set; }
       
        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }
    }
}
