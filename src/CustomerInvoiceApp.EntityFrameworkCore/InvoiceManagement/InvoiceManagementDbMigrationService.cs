using CustomerInvoiceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CustomerInvoiceApp.InvoiceManagement
{
	public class InvoiceManagementDbMigrationService : ICustomerInvoiceAppDbSchemaMigrator, ITransientDependency                 
	{
		private readonly IServiceProvider _serviceProvider;

		public InvoiceManagementDbMigrationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task MigrateAsync()
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<InvoiceManagementDbContext>();
				await dbContext.Database.MigrateAsync();
			}
		}
	}

}
