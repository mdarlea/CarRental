namespace CarRental.Identity.Data.Tests
{
	using CarRental.Data.UnitOfWork;
	using CarRental.Tests;
	using MediatR;
	using Microsoft.Data.Sqlite;
	using Microsoft.EntityFrameworkCore;
	using Moq;
	using System;
	using System.Data.Common;

	public class CarRentalUnitOfWorkFactory : IDisposable
	{
		private DbConnection connection;

		private DbContextOptions<CarRentalUnitOfWork> CreateOptions()
		{
			return new DbContextOptionsBuilder<CarRentalUnitOfWork>()				
				.UseSqlite(connection)
				.Options;

		}

		public CarRentalUnitOfWork CreateContext(bool log)
		{
			var mock = new Mock<IMediator>();

			if (connection == null)
			{
				connection = new SqliteConnection("DataSource=:memory:");
				connection.Open();

				var options = CreateOptions();
				using (var context = new CarRentalUnitOfWork(options, mock.Object))
				{
					context.Database.EnsureCreated();
				}
			}

			var dbContext = new CarRentalUnitOfWork(CreateOptions(), mock.Object);

			if (log)
			{
				dbContext.ConfigureLogging(s => Console.WriteLine(s), LoggingCategories.SQL);
			}
			return dbContext;
		}

		public void Dispose()
		{
			if (connection != null)
			{
				connection.Dispose();
				connection = null;
			}
		}
	}
}
