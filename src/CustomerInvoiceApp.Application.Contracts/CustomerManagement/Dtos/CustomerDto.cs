using System;
using Volo.Abp.Application.Dtos;

namespace CustomerInvoiceApp.CustomerManagement.Dtos
{
	public class CustomerDto : EntityDto<Guid>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public AddressDto BillingAddress { get; set; }
	}
}
