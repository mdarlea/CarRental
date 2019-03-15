namespace CarRental.ReadModel
{	
	public class Car
	{		
		public int Id { get; set; }

		public string Name { get; set; }
			
		public string TransmissionType { get; set; }
				
		public int NumberOfDoors { get; set; }

		public int NumberOfBags { get; set; }

		public int NumberOfSeats { get; set; }

		public bool HasPetrol { get; set; }

		public bool HasDiesel { get; set; }

		public bool HasAirConditioning { get; set; }
				
		public string ImageUrl { get; set; }
				
		public string Type { get; set; }		
	}
}
