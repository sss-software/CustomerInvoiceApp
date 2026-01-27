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

		Configure<AbpDbConnectionOptions>(options =>
		{
			options.ConnectionStrings.Default =
				configuration.GetConnectionString("Default");
		});

		context.Services.AddTransient<ICustomerInvoiceAppDbSchemaMigrator, InvoiceManagementDbMigrationService>();
		context.Services.AddTransient<ICustomerInvoiceAppDbSchemaMigrator, CustomerManagementDbMigrationService>();
		context.Services.AddTransient<CustomerInvoiceAppDbMigrationService>();
	}
}
