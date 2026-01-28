namespace CustomerInvoiceApp.Permissions
{
	public static class CustomerManagementPermissions
	{
		public const string GroupName = "CustomerManagement";
		public static class Customers
		{
			public const string Default = GroupName + ".Customers";
			public const string Create = "Customers" + ".Create";
			public const string Delete = "Customers" + ".Delete";
			public const string Update = "Customers" + ".Update";
		}
	}
}
