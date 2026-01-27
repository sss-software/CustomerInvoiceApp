using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace CustomerInvoiceApp.CustomerManagement.ValueObjects
{
	public class Address : ValueObject
	{
		public string Street { get; }
		public string City { get; }
		public string PostalCode { get; }

		protected Address() { }

		public Address(string street, string city, string postalCode)
		{
			Street = street;
			City = city;
			PostalCode = postalCode;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Street;
			yield return City;
			yield return PostalCode;
		}
	}
}
