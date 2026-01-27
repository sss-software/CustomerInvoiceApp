import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateInvoiceDto {
  customerId?: string;
  invoiceDate?: string;
  number?: string;
  paidUp?: boolean | null;
  lines?: CreateUpdateInvoiceLineDto[];
}

export interface CreateUpdateInvoiceLineDto {
  id?: string | null;
  productId?: string;
  description?: string;
  quantity?: number;
  unitPrice?: number;
}

export interface InvoiceDto extends EntityDto<string> {
  customerId?: string;
  invoiceDate?: string;
  number?: string;
  paidUp?: boolean | null;
  customerName?: string;
  lines?: InvoiceLineDto[];
}

export interface InvoiceLineDto extends EntityDto<string> {
  id?: string | null;
  productId?: string;
  description?: string;
  quantity?: number;
  unitPrice?: number;
  total?: number;
}

export interface InvoiceSearchDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
}
