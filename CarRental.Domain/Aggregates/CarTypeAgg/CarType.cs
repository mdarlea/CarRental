namespace CarRental.Domain.Aggregates.CarTypeAgg
{
	using Swaksoft.Domain.Seedwork.Aggregates;
	using System.ComponentModel.DataAnnotations;
	
	public class CarType: Entity
	{
		[Required]
		[MaxLength(150)]
		public string Type { get; set; }
	}
}
