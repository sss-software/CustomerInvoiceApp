namespace CustomerInvoiceApp.CustomerManagement.Dtos
{
	public class CreateUpdateCustomerDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public AddressDto BillingAddress { get; set; }
	}
}
