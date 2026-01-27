using AutoMapper;
using CustomerInvoiceApp.CustomerManagement.Dtos;
using CustomerInvoiceApp.CustomerManagement.Entities;
using CustomerInvoiceApp.CustomerManagement.ValueObjects;

namespace CustomerInvoiceApp.CustomerManagement.Mappers
{
	public class CustomerManagementApplicationAutoMapperProfile : Profile
	{
		public CustomerManagementApplicationAutoMapperProfile()
		{
			CreateMap<Address, AddressDto>();

			CreateMap<AddressDto, Address>()
				.ConstructUsing(dto =>
					dto == null
						? null!
						: new Address(dto.Street, dto.City, dto.PostalCode)
				);
			CreateMap<Customer, CustomerDto>()
				.ForMember(
					dest => dest.BillingAddress,
					opt => opt.MapFrom(src => src.BillingAddress)
				);
		}
	}
}
