using System;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class CreateUpdateInvoiceLineDto
	{
		public Guid ProductId { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
