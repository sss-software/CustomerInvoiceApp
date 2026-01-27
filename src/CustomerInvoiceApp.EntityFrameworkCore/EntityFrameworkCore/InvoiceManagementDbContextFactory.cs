using CustomerInvoiceApp.InvoiceManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CustomerInvoiceApp.EntityFrameworkCore
{
	public class InvoiceManagementDbContextFactory : IDesignTimeDbContextFactory<InvoiceManagementDbContext>
	{
		public InvoiceManagementDbContext CreateDbContext(string[] args)
		{
			var configuration = BuildConfiguration();
			var builder = new DbContextOptionsBuilder<InvoiceManagementDbContext>()
				.UseSqlServer(configuration.GetConnectionString("Default"));

			return new InvoiceManagementDbContext(builder.Options);
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
