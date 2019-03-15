namespace CarRental.Domain.Aggregates.AvailableCarAgg
{
	using Swaksoft.Domain.Seedwork.Aggregates;
	using System.Threading.Tasks;

	public interface IAvailableCarRepository: IRepository<AvailableCar>
	{
		Task<AvailableCar> GetAvailableCar(int id);
	}
}
