using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace CustomerInvoiceApp.InvoiceManagement.Entities
{
	public class InvoiceLine : Entity<Guid>
	{
		public Guid ProductId { get; private set; }
		public string Description { get; private set; }
		public int Quantity { get; private set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; private set; }
		public decimal Total => Quantity * UnitPrice;

		public InvoiceLine(Guid id, Guid productId, string description, int quantity, decimal unitPrice) : base(id)
		{
			ProductId = productId;
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Quantity = quantity;
			UnitPrice = unitPrice;
		}

		public void UpdateLine(Guid productId, string description, int quantity, decimal unitPrice)
		{
			ProductId = productId;

			if (string.IsNullOrWhiteSpace(description))
				throw new ArgumentException("Description cannot be empty", nameof(description));

			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));

			if (unitPrice < 0)
				throw new ArgumentException("UnitPrice cannot be negative", nameof(unitPrice));

			Description = description;
			Quantity = quantity;
			UnitPrice = unitPrice;
		}
	}
}
