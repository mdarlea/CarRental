namespace CarRental.ReadModel
{
	using Swaksoft.Core.Dto;

	public class AvailableCarResult : ActionResult
	{		
		public virtual Car Car { get; set; }		

		public decimal Price { get; set; }		
	}
}
