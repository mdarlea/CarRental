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
        
        [MaxLength(50)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [MaxLength(30)]
        public string State { get; set; }

		[Required]
        [MaxLength(20)]        
        public string Zip { get; set; }        
    }
}
