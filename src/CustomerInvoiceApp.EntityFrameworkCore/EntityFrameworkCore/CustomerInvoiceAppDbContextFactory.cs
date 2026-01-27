using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CustomerInvoiceApp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CustomerInvoiceAppDbContextFactory : IDesignTimeDbContextFactory<CustomerInvoiceAppDbContext>
{
    public CustomerInvoiceAppDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        CustomerInvoiceAppEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<CustomerInvoiceAppDbContext>()
			.UseSqlServer(configuration.GetConnectionString("Default"));

		return new CustomerInvoiceAppDbContext(builder.Options);
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
