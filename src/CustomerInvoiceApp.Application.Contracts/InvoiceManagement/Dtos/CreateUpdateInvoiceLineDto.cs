using System;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class CreateUpdateInvoiceLineDto
	{
		public Guid? Id { get; set; }
		public Guid ProductId { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
