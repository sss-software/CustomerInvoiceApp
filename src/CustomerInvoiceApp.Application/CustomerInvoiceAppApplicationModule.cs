using CustomerInvoiceApp.EntityFrameworkCore;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace CustomerInvoiceApp;

[DependsOn(
    typeof(CustomerInvoiceAppDomainModule),
    typeof(CustomerInvoiceAppApplicationContractsModule),
	typeof(CustomerInvoiceAppEntityFrameworkCoreModule),
	typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class CustomerInvoiceAppApplicationModule : AbpModule
{

}
