using CustomerInvoiceApp.InvoiceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CustomerInvoiceApp.InvoiceManagement.Repositories
{
	public class InvoiceRepository : EfCoreRepository<InvoiceManagementDbContext, Invoice, Guid>, IInvoiceRepository
	{
		public InvoiceRepository(IDbContextProvider<InvoiceManagementDbContext> dbContextProvider)
			: base(dbContextProvider)
		{
		}

		public async Task<Invoice> GetWithLinesAsync(Guid invoiceId)
		{
			return await DbSet
				.Include(i => i.Lines)
				.FirstOrDefaultAsync(i => i.Id == invoiceId);
		}
	}
}
