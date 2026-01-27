import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateInvoiceDto {
  customerId?: string;
  invoiceDate?: string;
  number?: string;
  lines?: CreateUpdateInvoiceLineDto[];
}

export interface CreateUpdateInvoiceLineDto {
  productId?: string;
  description?: string;
  quantity?: number;
  unitPrice?: number;
}

export interface InvoiceDto extends EntityDto<string> {
  customerId?: string;
  invoiceDate?: string;
  number?: string;
  customerName?: string;
  lines?: InvoiceLineDto[];
}

export interface InvoiceLineDto extends EntityDto<string> {
  productId?: string;
  description?: string;
  quantity?: number;
  unitPrice?: number;
  total?: number;
}

export interface InvoiceSearchDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
}
