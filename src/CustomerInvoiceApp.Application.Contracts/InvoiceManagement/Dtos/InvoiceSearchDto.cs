using Volo.Abp.Application.Dtos;

namespace CustomerInvoiceApp.InvoiceManagement.Dtos
{
	public class InvoiceSearchDto : PagedAndSortedResultRequestDto
	{
		public string? Filter { get; set; }
	}
}
