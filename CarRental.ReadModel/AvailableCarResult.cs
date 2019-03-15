namespace CarRental.ReadModel
{
	using Swaksoft.Core.Dto;

	public class AvailableCarResult : ActionResult
	{	
		public int Id { get; set; }
		public virtual Car Car { get; set; }		

		public decimal Price { get; set; }		
	}
}
