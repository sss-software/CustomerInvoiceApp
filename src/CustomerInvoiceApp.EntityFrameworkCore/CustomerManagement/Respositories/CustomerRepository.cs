using CustomerInvoiceApp.CustomerManagement.Entities;
using CustomerInvoiceApp.CustomerManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CustomerInvoiceApp.CustomerManagement.Respositories
{
	public class CustomerRepository : EfCoreRepository<CustomerManagementDbContext, Customer, Guid>, ICustomerRepository
	{
		public CustomerRepository(IDbContextProvider<CustomerManagementDbContext> dbContextProvider)
			: base(dbContextProvider)
		{
		}

		public async Task<Customer> GetByEmailAsync(string email)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Email == email);
		}
	}
}
