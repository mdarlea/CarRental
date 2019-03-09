namespace CarRental.Identity.Data.Tests
{
	using CarRental.Tests;
	using Microsoft.Data.Sqlite;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Data.Common;

	public class ApplicationDbContextFactory: IDisposable
	{
		private DbConnection connection;

		private DbContextOptions<ApplicationDbContext> CreateOptions()
		{
			return new DbContextOptionsBuilder<ApplicationDbContext>()			
				.UseSqlite(connection)
				.Options;

		}

		public ApplicationDbContext CreateContext(bool log)
		{
			if (connection == null)
			{
				connection = new SqliteConnection("DataSource=:memory:");
				connection.Open();

				var options = CreateOptions();
				using (var context = new ApplicationDbContext(options))
				{
					context.Database.EnsureCreated();
				}
			}

			var dbContext = new ApplicationDbContext(CreateOptions());

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
