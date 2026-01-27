using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(typeof(AbpDddDomainModule))]
	public class InvoiceManagementDomainModule : AbpModule
	{
	}
}
