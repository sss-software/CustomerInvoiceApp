using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(
		typeof(InvoiceManagementDomainModule),
		typeof(InvoiceManagementApplicationContractsModule),
		typeof(InvoiceManagementEntityFrameworkCoreModule),
		typeof(AbpDddApplicationModule),
		typeof(AbpAutoMapperModule),
		typeof(AbpIdentityApplicationModule),
		typeof(AbpPermissionManagementApplicationModule)
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
