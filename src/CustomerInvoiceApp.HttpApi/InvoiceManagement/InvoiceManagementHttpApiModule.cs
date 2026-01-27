using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(typeof(AbpAspNetCoreMvcModule))]
	public class InvoiceManagementHttpApiModule : AbpModule
	{
	}
}
