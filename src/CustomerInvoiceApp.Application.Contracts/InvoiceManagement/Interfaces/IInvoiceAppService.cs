using CustomerInvoiceApp.InvoiceManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CustomerInvoiceApp.InvoiceManagement.Interfaces
{
	public interface IInvoiceAppService : IApplicationService
	{
		Task<InvoiceDto> GetAsync(Guid id);
		Task<PagedResultDto<InvoiceDto>> GetListAsync(InvoiceSearchDto input);
		Task<InvoiceDto> CreateAsync(CreateUpdateInvoiceDto input);
		Task<InvoiceDto> UpdateAsync(Guid id, CreateUpdateInvoiceDto input);
		Task DeleteAsync(Guid id);
		Task<List<InvoiceDto>> GetByCustomerAsync(Guid customerId);
		Task<InvoiceDto> MarkAsPaidAsync(Guid id);
	}
}
