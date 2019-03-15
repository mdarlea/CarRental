namespace CarRental.Data.Repositories
{
	using CarRental.Data.UnitOfWork;
	using CarRental.Domain.Aggregates.UserAgg;	
	using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
	using System;


	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(CarRentalUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public User GetUser(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			return GetSet().Find(userId);
		}
	}
}
