using CustomerInvoiceApp.CustomerManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CustomerInvoiceApp.EntityFrameworkCore
{
	public class CustomerManagementDbContextFactory : IDesignTimeDbContextFactory<CustomerManagementDbContext>
	{
		public CustomerManagementDbContext CreateDbContext(string[] args)
		{
			var configuration = BuildConfiguration();
			var builder = new DbContextOptionsBuilder<CustomerManagementDbContext>()
				.UseSqlServer(configuration.GetConnectionString("Default"));

			return new CustomerManagementDbContext(builder.Options);
		}

		private static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CustomerInvoiceApp.DbMigrator/"))
				.AddJsonFile("appsettings.json", optional: false)
				.AddEnvironmentVariables();

			return builder.Build();
		}
	}
}
