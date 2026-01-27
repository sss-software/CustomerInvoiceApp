using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(
		typeof(InvoiceManagementDomainModule),
		typeof(InvoiceManagementApplicationContractsModule),
		typeof(InvoiceManagementEntityFrameworkCoreModule),
		typeof(AbpDddApplicationModule),
		typeof(AbpAutoMapperModule)
	)]
	public class InvoiceManagementApplicationModule : AbpModule
	{
		public override void ConfigureServices(ServiceConfigurationContext context)
		{
			Configure<AbpAutoMapperOptions>(options =>
			{
				options.AddMaps<InvoiceManagementApplicationModule>(validate: true);
			});
		}
	}
}
