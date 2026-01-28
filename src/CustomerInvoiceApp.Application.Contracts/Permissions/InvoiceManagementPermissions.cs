namespace CustomerInvoiceApp.Permissions
{
	public static class InvoiceManagementPermissions
	{
		public const string GroupName = "InvoiceManagement";
		public static class Invoices
		{
			public const string Default = GroupName + ".Invoices";
			public const string Create = "Invoices" + ".Create";
			public const string Update = "Invoices" + ".Update";
			public const string Delete = "Invoices" + ".Delete";
			public const string MarkPaid = "Invoices" + ".MarkPaid";
		}
	}
}
