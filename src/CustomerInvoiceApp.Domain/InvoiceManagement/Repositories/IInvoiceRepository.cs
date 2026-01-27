using CustomerInvoiceApp.InvoiceManagement.Entities;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CustomerInvoiceApp.InvoiceManagement.Repositories
{
	public interface IInvoiceRepository : IRepository<Invoice, Guid>
	{
		Task<Invoice> GetWithLinesAsync(Guid invoiceId);
	}
}
