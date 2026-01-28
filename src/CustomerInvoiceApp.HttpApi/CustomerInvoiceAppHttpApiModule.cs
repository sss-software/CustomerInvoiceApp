using Localization.Resources.AbpUi;
using CustomerInvoiceApp.Localization;
using Volo.Abp.Account;
using Volo.Abp.SettingManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.Localization;
using Volo.Abp.TenantManagement;
using CustomerInvoiceApp.CustomerManagement;
using CustomerInvoiceApp.InvoiceManagement;

namespace CustomerInvoiceApp;

 [DependsOn(
    typeof(CustomerInvoiceAppApplicationContractsModule),
	typeof(CustomerManagementApplicationModule), 
	typeof(InvoiceManagementApplicationModule),
	typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule)
    )]
public class CustomerInvoiceAppHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
		Configure((System.Action<AbpLocalizationOptions>)(options =>
        {
            options.Resources
                .Get<Localization.CustomerInvoiceAppResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        }));
    }
}
