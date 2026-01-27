import { PagedResultDto } from '@abp/ng.core';
import { inject, Injectable } from '@angular/core';
import { InvoiceService } from '@proxy/invoice-management';
import { InvoiceSearchDto, InvoiceDto, CreateUpdateInvoiceDto } from '@proxy/invoice-management/dtos';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceProxyService {
  private api = inject(InvoiceService);

  // Get paged list of invoices
  getList(input: InvoiceSearchDto): Observable<PagedResultDto<InvoiceDto>> {
    return this.api.getList(input);
  }

  // Get a single invoice by ID
  get(id: string) {
    return this.api.get(id);
  }

  // Create a new invoice
  create(input: CreateUpdateInvoiceDto) {
    return this.api.create(input);
  }

  // Update an existing invoice
  update(id: string, input: CreateUpdateInvoiceDto) {
    return this.api.update(id, input);
  }

  // Delete an invoice
  delete(id: string) {
    return this.api.delete(id);
  } 
}
