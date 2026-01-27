using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.CustomerManagement
{

	[DependsOn(
		typeof(CustomerManagementDomainModule),
		typeof(CustomerManagementApplicationContractsModule),
		typeof(CustomerManagementEntityFrameworkCoreModule),
		typeof(AbpDddApplicationModule),
		typeof(AbpAutoMapperModule)
	)]
	public class CustomerManagementApplicationModule : AbpModule
	{
		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			Configure<AbpAutoMapperOptions>(options =>
			{
				options.AddMaps<CustomerManagementApplicationModule>(validate: true);
			});
		}
	}
}
