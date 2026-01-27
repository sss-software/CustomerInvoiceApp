import type { CreateUpdateInvoiceDto, InvoiceDto, InvoiceSearchDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateUpdateInvoiceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InvoiceDto>({
      method: 'POST',
      url: '/api/app/invoice',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/invoice/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InvoiceDto>({
      method: 'GET',
      url: `/api/app/invoice/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByCustomer = (customerId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InvoiceDto[]>({
      method: 'GET',
      url: `/api/app/invoice/by-customer/${customerId}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: InvoiceSearchDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<InvoiceDto>>({
      method: 'GET',
      url: '/api/app/invoice',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateInvoiceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, InvoiceDto>({
      method: 'PUT',
      url: `/api/app/invoice/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}