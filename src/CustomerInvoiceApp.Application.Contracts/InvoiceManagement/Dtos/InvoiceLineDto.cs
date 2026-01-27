using System;
using Volo.Abp.Application.Dtos;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class InvoiceLineDto : EntityDto<Guid>
	{
		public Guid ProductId { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total => Quantity * UnitPrice;
	}



}
