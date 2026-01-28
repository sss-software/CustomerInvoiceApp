using CustomerInvoiceApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CustomerInvoiceApp.Permissions
{
	public class InvoiceManagementPermissionDefinitionProvider : PermissionDefinitionProvider
	{
		public override void Define(IPermissionDefinitionContext context)
		{
			var customersGroup = context.AddGroup(InvoiceManagementPermissions.GroupName, L("Permission:Invoices")
			);

			var invoicesPermission = customersGroup.AddPermission(InvoiceManagementPermissions.Invoices.Default, L("Permission:Invoices")
			);

			invoicesPermission.AddChild(InvoiceManagementPermissions.Invoices.Create, L("Permission:Invoices.Create")
			);

			invoicesPermission.AddChild(InvoiceManagementPermissions.Invoices.Delete, L("Permission:Invoices.Delete")
			);

			invoicesPermission.AddChild(InvoiceManagementPermissions.Invoices.Update, L("Permission:Invoices.Update")
			);
		}

		private static LocalizableString L(string name)
		{
			return LocalizableString.Create<LocalAppResource>(name);
		}
	}
}
