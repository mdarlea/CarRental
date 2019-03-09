namespace CarRental.Domain.Aggregates.UserAgg
{
	using Swaksoft.Domain.Seedwork.Aggregates;
	using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("aspnetusers")]
	public class User: Entity<string>
	{
		[Required]
		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		[Required]
		public virtual Name Name { get; set; }
	}
}
