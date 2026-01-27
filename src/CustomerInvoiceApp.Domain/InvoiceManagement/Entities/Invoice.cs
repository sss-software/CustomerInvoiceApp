using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace CustomerInvoiceApp.InvoiceManagement.Entities
{
	public class Invoice : AggregateRoot<Guid>
	{
		public Guid CustomerId { get; private set; }
		public DateTime InvoiceDate { get; private set; }
		public string Number { get; private set; }

		[NotMapped]
		public string CustomerName { get; private set; }
		public List<InvoiceLine> Lines { get; private set; } = new();

		public Invoice(Guid id, Guid customerId, DateTime invoiceDate, string number) : base(id)
		{
			CustomerId = customerId;
			InvoiceDate = invoiceDate;
			Number = number ?? throw new ArgumentNullException(nameof(number));
		}

		public void UpdateInvoiceDate(DateTime invoiceDate)
		{
			InvoiceDate = invoiceDate;
		}

		public void UpdateCustomer(Guid customerId)
		{
			CustomerId = customerId;
		}

		public void SetCustomerName(string name)
		{
			CustomerName = name;
		}

		public void AddLine(Guid productId, string description, int quantity, decimal unitPrice)
		{
			Lines.Add(new InvoiceLine(Guid.NewGuid(), productId, description, quantity, unitPrice));
		}
	}
}
