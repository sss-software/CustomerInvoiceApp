using System;
using System.Collections.Generic;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class CreateUpdateInvoiceDto
	{
		public Guid CustomerId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public string Number { get; set; }
		public List<CreateUpdateInvoiceLineDto> Lines { get; set; } = new();
	}
}
