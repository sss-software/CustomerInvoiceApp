using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.CustomerManagement
{
	[DependsOn(
		typeof(CustomerManagementDomainModule),
		typeof(AbpEntityFrameworkCoreModule)
	)]
	public class CustomerManagementEntityFrameworkCoreModule : AbpModule
	{
		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			context.Services.AddAbpDbContext<CustomerManagementDbContext>(options =>
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
