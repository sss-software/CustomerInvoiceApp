using CustomerInvoiceApp.CustomerManagement.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CustomerInvoiceApp.CustomerManagement
{
	public interface ICustomerAppService : IApplicationService
	{
		Task<CustomerDto> GetAsync(Guid id);
		Task<PagedResultDto<CustomerDto>> GetListAsync(CustomerSearchDto input);
		Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input);
		Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input);
		Task DeleteAsync(Guid id);
	}
}
