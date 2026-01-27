using CustomerInvoiceApp.CustomerManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CustomerInvoiceApp.Data
{
	public class CustomerManagementDbMigrationService : ICustomerInvoiceAppDbSchemaMigrator, ITransientDependency
	{
		private readonly IServiceProvider _serviceProvider;
		public CustomerManagementDbMigrationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task MigrateAsync()
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<CustomerManagementDbContext>();
				await dbContext.Database.MigrateAsync();
			}
		}
	}
}
