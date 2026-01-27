using AutoMapper;
using CustomerInvoiceApp.CustomerManagement.Dtos;
using CustomerInvoiceApp.CustomerManagement.Entities;
using CustomerInvoiceApp.CustomerManagement.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CustomerInvoiceApp.CustomerManagement
{
	public class CustomerAppService : ApplicationService, ICustomerAppService
	{
		private readonly IRepository<Customer, Guid> _repository;
		private readonly IMapper _mapper;

		public CustomerAppService(IRepository<Customer, Guid> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<CustomerDto> GetAsync(Guid id)
		{
			var entity = await _repository.GetAsync(id);
			var dto = _mapper.Map<CustomerDto>(entity);
			return dto;
		}

		public async Task<PagedResultDto<CustomerDto>> GetListAsync(CustomerSearchDto input)
		{
			IQueryable<Customer> query = await _repository.GetQueryableAsync();

			if (!string.IsNullOrWhiteSpace(input.Filter))
			{
				query = query.Where(c =>
					c.Name.Contains(input.Filter) ||
					c.Email.Contains(input.Filter) ||
					c.Phone.Contains(input.Filter) ||
					c.BillingAddress.Street.Contains(input.Filter) ||
					c.BillingAddress.City.Contains(input.Filter) ||
					c.BillingAddress.PostalCode.Contains(input.Filter));
			}

			var total = await AsyncExecuter.CountAsync(query);

			var sorting = string.IsNullOrWhiteSpace(input.Sorting) ? "Name" : input.Sorting;
			query = query.OrderBy(sorting);

			query = query.Skip(input.SkipCount).Take(input.MaxResultCount);

			var list = await AsyncExecuter.ToListAsync(query);
			var dtoList = _mapper.Map<List<CustomerDto>>(list);

			return new PagedResultDto<CustomerDto>(total, dtoList);
		}

		public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
		{
			var address = new Address(
				input.BillingAddress.Street,
				input.BillingAddress.City,
				input.BillingAddress.PostalCode
			);

			var customer = new Customer(
				GuidGenerator.Create(),
				input.Name,
				input.Email,
				input.Phone,
				address
			);

			await _repository.InsertAsync(customer, autoSave: true);
			return _mapper.Map<CustomerDto>(customer);
		}

		public async Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
		{
			var customer = await _repository.GetAsync(id);

			customer.UpdateEmail(input.Email);
			customer.UpdatePhone(input.Phone);

			var address = new Address(
				input.BillingAddress.Street,
				input.BillingAddress.City,
				input.BillingAddress.PostalCode
			);

			customer.UpdateBillingAddress(address);

			await _repository.UpdateAsync(customer, autoSave: true);

			var dto = _mapper.Map<CustomerDto>(customer);

			return dto;
		}

		public async Task DeleteAsync(Guid id)
		{
			var customer = await _repository.FindAsync(id);
			if (customer == null)
			{
				throw new UserFriendlyException("Customer not found");
			}

			await _repository.DeleteAsync(customer, autoSave: true);
		}
	}
}
