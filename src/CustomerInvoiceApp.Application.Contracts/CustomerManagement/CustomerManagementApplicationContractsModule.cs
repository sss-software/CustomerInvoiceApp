using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.CustomerManagement
{
	[DependsOn(typeof(AbpDddApplicationContractsModule))]
	public class CustomerManagementApplicationContractsModule : AbpModule
	{
	}
}
