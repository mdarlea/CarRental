namespace CarRental.Domain.Aggregates.UserAgg
{
	using Swaksoft.Domain.Seedwork.Aggregates;
	
	public interface IUserRepository: IRepository<User>
	{
		User GetUser(string userId);
	}
}
