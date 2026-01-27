using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(
		typeof(InvoiceManagementDomainModule),
		typeof(AbpEntityFrameworkCoreModule)
	)]
	public class InvoiceManagementEntityFrameworkCoreModule : AbpModule
	{
		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			context.Services.AddAbpDbContext<InvoiceManagementDbContext>(options =>
			{
				options.AddDefaultRepositories(includeAllEntities: true);
			});

			Configure<AbpDbContextOptions>(options =>
			{
				options.UseSqlServer(); 
			});
		}
	}
}
