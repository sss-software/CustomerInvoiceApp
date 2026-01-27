using CustomerInvoiceApp.CustomerManagement.ValueObjects;
using CustomerInvoiceApp.InvoiceManagement.Entities;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CustomerInvoiceApp.CustomerManagement.Entities
{
	public class Customer : AggregateRoot<Guid>
	{
		public string Name { get; private set; }
		public string Email { get; private set; }
		public string Phone { get; private set; }
		public Address BillingAddress { get; private set; }
		public virtual List<Invoice> Invoices { get; private set; } = new();

		protected Customer() { }

		public Customer(Guid id, string name, string email, string phone, Address billingAddress)
			: base(id)
		{
			Name = name;
			Email = email;
			Phone = phone;
			BillingAddress = billingAddress;
		}

		public void UpdateEmail(string newEmail)
		{
			if (string.IsNullOrWhiteSpace(newEmail))
				throw new ArgumentException("Email cannot be empty", nameof(newEmail));

			Email = newEmail;
		}

		public void UpdatePhone(string newPhone)
		{
			if (string.IsNullOrWhiteSpace(newPhone))
				throw new ArgumentException("Phone cannot be empty", nameof(newPhone));

			Phone = newPhone;
		}

		public void UpdateBillingAddress(Address newAddress)
		{
			BillingAddress = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
		}
	}
}
