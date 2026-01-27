using CustomerInvoiceApp.CustomerManagement.Entities;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CustomerInvoiceApp.CustomerManagement.Repositories
{
	public interface ICustomerRepository : IRepository<Customer, Guid>
	{
		Task<Customer> GetByEmailAsync(string email);
	}
}
