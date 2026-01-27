using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(typeof(AbpDddDomainModule))]
	public class InvoiceManagementDomainModule : AbpModule
	{
	}
}
