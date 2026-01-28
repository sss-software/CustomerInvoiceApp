using CustomerInvoiceApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CustomerInvoiceApp.Permissions
{
	public class CustomerManagementPermissionDefinitionProvider : PermissionDefinitionProvider
	{
		public override void Define(IPermissionDefinitionContext context)
		{
			var customersGroup = context.AddGroup(CustomerManagementPermissions.GroupName, L("Permission:Customers")
			);

			var invoicesPermission = customersGroup.AddPermission(CustomerManagementPermissions.Customers.Default,L("Permission:Customers")
			);

			invoicesPermission.AddChild(CustomerManagementPermissions.Customers.Create, L("Permission:Customers.Create")
			);

			invoicesPermission.AddChild(CustomerManagementPermissions.Customers.Delete, L("Permission:Customers.Delete")
			);

			invoicesPermission.AddChild(CustomerManagementPermissions.Customers.Update, L("Permission:Customers.Update")
			);
		}

		private static LocalizableString L(string name)
		{
			return LocalizableString.Create<LocalAppResource>(name);
		}
	}
}
