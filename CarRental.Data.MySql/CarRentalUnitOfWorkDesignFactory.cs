namespace CarRental.Data.MySql
{
	using CarRental.Data.UnitOfWork;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;

	public class CarRentalUnitOfWorkDesignFactory : IDesignTimeDbContextFactory<CarRentalUnitOfWork>
	{
		public CarRentalUnitOfWork CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CarRentalUnitOfWork>()
			   .UseMySQL("Data Source=mysql502.discountasp.net; port=3306; Initial Catalog=MYSQL5_948078_carrental; uid=carrentaluser; pwd=demo2019;",
			   b => b.MigrationsAssembly(GetType().Assembly.FullName));

			return new CarRentalUnitOfWork(optionsBuilder.Options, new NoMediator());
		}
	}
}
