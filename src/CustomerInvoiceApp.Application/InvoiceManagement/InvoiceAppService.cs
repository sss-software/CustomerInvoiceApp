using AutoMapper;
using CustomerInvoiceApp.CustomerManagement.Entities;
using CustomerInvoiceApp.InvoiceManagement.Dtos;
using CustomerInvoiceApp.InvoiceManagement.Entities;
using CustomerInvoiceApp.InvoiceManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace CustomerInvoiceApp.InvoiceManagement
{
	public class InvoiceAppService : ApplicationService, IInvoiceAppService
	{
		private readonly IRepository<Invoice, Guid> _repository;
		private readonly IRepository<Customer, Guid> _customerRepository;
		private readonly IMapper _mapper;

		public InvoiceAppService(IRepository<Invoice, Guid> repository, IRepository<Customer, Guid> customerRepository, IMapper mapper)
		{
			_repository = repository;
			_customerRepository = customerRepository;
			_mapper = mapper;
		}

		public async Task<InvoiceDto> GetAsync(Guid id)
		{
			// Step 1: Get the invoice entity including lines
			var queryable = await _repository.GetQueryableAsync(); // Step 1: get IQueryable<Invoice>
			var invoice = await queryable
				.Include(i => i.Lines)        // Step 2: Include lines
				.FirstOrDefaultAsync(i => i.Id == id);

			if (invoice == null)
			{
				throw new UserFriendlyException("Invoice not found");
			}

			// Step 2: Get customer name
			var customer = await _customerRepository.GetAsync(invoice.CustomerId);

			// Step 3: Map to DTO
			var dto = _mapper.Map<InvoiceDto>(invoice);
			dto.CustomerName = customer.Name; // Add the customer name

			return dto;
		}


		//public async Task<InvoiceDto> GetAsync(Guid id)
		//{
		//	var queryable = await _repository.GetQueryableAsync();

		//	var entity = await queryable
		//		.Include(i => i.Lines) 
		//		.FirstOrDefaultAsync(i => i.Id == id);

		//	if (entity == null)
		//	{
		//		throw new UserFriendlyException("Invoice not found");
		//	}

		//	return _mapper.Map<InvoiceDto>(entity);
		//}


		public async Task<PagedResultDto<InvoiceDto>> GetListAsync(InvoiceSearchDto input)
		{
			var query = await _repository.GetQueryableAsync();

			// Filter invoices
			if (!string.IsNullOrWhiteSpace(input.Filter))
			{
				query = query.Where(i =>
					i.Number.Contains(input.Filter) ||
					i.InvoiceDate.ToString().Contains(input.Filter)
				);
			}

			var total = await AsyncExecuter.CountAsync(query);

			// Apply sorting
			var sorting = string.IsNullOrWhiteSpace(input.Sorting) ? "Number" : input.Sorting;
			query = query.OrderBy(sorting);

			// Paging
			var invoicesPaged = query.Skip(input.SkipCount).Take(input.MaxResultCount);

			var invoiceList = await AsyncExecuter.ToListAsync(invoicesPaged);

			// Get customer IDs
			var customerIds = invoiceList.Select(i => i.CustomerId).Distinct().ToList();

			var customers = await _customerRepository.GetListAsync(c => customerIds.Contains(c.Id));
			var customerDict = customers.ToDictionary(c => c.Id, c => c.Name);

			// Map to DTO
			var dtoList = invoiceList.Select(i => new InvoiceDto
			{
				Id = i.Id,
				Number = i.Number,
				InvoiceDate = i.InvoiceDate,
				CustomerId = i.CustomerId,
				CustomerName = customerDict.ContainsKey(i.CustomerId) ? customerDict[i.CustomerId] : string.Empty,
				Lines = _mapper.Map<List<InvoiceLineDto>>(i.Lines)
			}).ToList();

			return new PagedResultDto<InvoiceDto>(total, dtoList);
		}


		public async Task<InvoiceDto> CreateAsync(CreateUpdateInvoiceDto input)
		{
			var customer = await _customerRepository.GetAsync(input.CustomerId);
			if (customer == null)
			{
				throw new UserFriendlyException("Customer not found");
			}
			var invoice = new Invoice(
				GuidGenerator.Create(),
				input.CustomerId,
				input.InvoiceDate,
				input.Number
			);

			foreach (var line in input.Lines)
			{
				invoice.AddLine(line.ProductId, line.Description, line.Quantity, line.UnitPrice);
			}

			await _repository.InsertAsync(invoice, autoSave: true);
			return _mapper.Map<InvoiceDto>(invoice);
		}

		public async Task<InvoiceDto> UpdateAsync(Guid id, CreateUpdateInvoiceDto input)
		{
			var invoice = await _repository.GetAsync(id);
			if (invoice == null)
			{
				throw new UserFriendlyException("Invoice not found");
			}
			invoice.UpdateCustomer(input.CustomerId);
			invoice.UpdateInvoiceDate(input.InvoiceDate);

			invoice.Lines.Clear();
			foreach (var line in input.Lines)
			{
				invoice.AddLine(line.ProductId, line.Description, line.Quantity, line.UnitPrice);
			}

			await _repository.UpdateAsync(invoice, autoSave: true);
			return _mapper.Map<InvoiceDto>(invoice);
		}

		public async Task DeleteAsync(Guid id)
		{
			var invoice = await _repository.FindAsync(id);
			if (invoice == null)
			{
				throw new UserFriendlyException("Invoice not found");
			}

			await _repository.DeleteAsync(invoice, autoSave: true);
		}

		public async Task<List<InvoiceDto>> GetByCustomerAsync(Guid customerId)
		{
			var query = await _repository.GetQueryableAsync();
			query = query.Where(i => i.CustomerId == customerId);

			var invoices = await AsyncExecuter.ToListAsync(query);
			return _mapper.Map<List<InvoiceDto>>(invoices);
		}
	}
}
