using CustomerInvoiceApp.CustomerManagement;
using CustomerInvoiceApp.Data;
using CustomerInvoiceApp.EntityFrameworkCore;
using CustomerInvoiceApp.InvoiceManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.DbMigrator;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(CustomerInvoiceAppEntityFrameworkCoreModule),
	typeof(InvoiceManagementEntityFrameworkCoreModule),
	typeof(CustomerManagementEntityFrameworkCoreModule),
	typeof(CustomerInvoiceAppApplicationContractsModule)
)]
public class CustomerInvoiceAppDbMigratorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();

		// Set the connection string for all DbContexts
		Configure<AbpDbConnectionOptions>(options =>
		{
			options.ConnectionStrings.Default =
				configuration.GetConnectionString("Default");
		});

		// Register module migration services
		context.Services.AddTransient<ICustomerInvoiceAppDbSchemaMigrator, InvoiceManagementDbMigrationService>();
		context.Services.AddTransient<ICustomerInvoiceAppDbSchemaMigrator, CustomerManagementDbMigrationService>();
		// Register the main migration service
		context.Services.AddTransient<CustomerInvoiceAppDbMigrationService>();
	}
}
