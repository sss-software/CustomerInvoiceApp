using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.CustomerManagement
{
	[DependsOn(typeof(AbpDddDomainModule))]
	public class CustomerManagementDomainModule : AbpModule
	{
	}
}
