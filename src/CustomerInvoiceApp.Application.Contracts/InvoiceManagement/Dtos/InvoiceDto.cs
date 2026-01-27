using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class InvoiceDto : EntityDto<Guid>
	{
		public Guid CustomerId { get; set; }
		public DateTime InvoiceDate { get; set; }

		public string Number { get; set; }

		public bool? PaidUp { get; set; }

		public string CustomerName { get; set; }
		public List<InvoiceLineDto> Lines { get; set; } = new();
	}
}
