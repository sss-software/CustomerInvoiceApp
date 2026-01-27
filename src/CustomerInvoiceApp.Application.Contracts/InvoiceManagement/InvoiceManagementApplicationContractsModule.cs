using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[DependsOn(typeof(AbpDddApplicationContractsModule))]
	public class InvoiceManagementApplicationContractsModule : AbpModule
	{
	}
}
