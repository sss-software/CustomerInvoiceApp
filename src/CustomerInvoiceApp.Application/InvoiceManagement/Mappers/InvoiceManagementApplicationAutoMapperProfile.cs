using AutoMapper;
using CustomerInvoiceApp.InvoiceManagement.Dtos;
using CustomerInvoiceApp.InvoiceManagement.Entities;

namespace CustomerInvoiceApp.InvoiceManagement.Mappers
{
	public class InvoiceManagementApplicationAutoMapperProfile : Profile
	{
		public InvoiceManagementApplicationAutoMapperProfile()
		{
			CreateMap<Invoice, InvoiceDto>();
			CreateMap<InvoiceLine, InvoiceLineDto>();
		}
	}
}
