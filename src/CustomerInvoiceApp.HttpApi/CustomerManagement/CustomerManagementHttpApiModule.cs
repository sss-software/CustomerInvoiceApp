using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.CustomerManagement
{
	[DependsOn(typeof(AbpAspNetCoreMvcModule))]
	public class CustomerManagementHttpApiModule : AbpModule
	{
	}
}
