namespace CarRental.Identity.Data.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CreditCard
	{
		public enum CreditCardType {
			VISA,
			MasterCard,
			AmericanExpress
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public CreditCardType Type { get; set; }

		[Required]
		public string CreditCardNumber { get; set; }

		[Required]
		public string NameOnCard { get; set; }

		[Required]
		public DateTime ExpirationTime { get; set; }

		public int BillingAddressId { get; set; }
		public virtual Address BillingAddress { get; set; }
	}
}
