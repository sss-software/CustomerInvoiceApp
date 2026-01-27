using Volo.Abp.Application.Dtos;

namespace CustomerInvoiceApp.CustomerManagement.Dtos
{
	public class CustomerSearchDto : PagedAndSortedResultRequestDto
	{
		public string? Filter { get; set; } 
	}
}
