import { ListService } from '@abp/ng.core';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { InvoiceDto, InvoiceSearchDto } from '@proxy/invoice-management/dtos';
import { InvoiceProxyService } from '../../services/invoice-proxy-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-invoice-list',
  imports: [CommonModule, FormsModule, RouterModule],
  standalone: true,
  providers: [ListService],
  templateUrl: './invoice-list.html',
  styleUrl: './invoice-list.scss'
})
export class InvoiceListComponent implements OnInit {
  invoices: InvoiceDto[] = [];
  totalCount = 0;

  selectedInvoiceId: string | null = null;

  // Search + sorting + paging state
  searchTerm = '';
  sortingField = 'InvoiceDate';
  sortingDirection: 'asc' | 'desc' = 'asc';
  page = 1;
  pageSize = 10;

  constructor() { }

  public readonly list: ListService = inject(ListService);
  private invoiceService = inject(InvoiceProxyService);
  private router: Router = inject(Router);

  ngOnInit(): void {
    // Connect ListService to proxy getList
    this.list.hookToQuery((query) => this.getList(query)).subscribe((result) => {
      this.invoices = result.items ?? [];
      this.totalCount = result.totalCount;
    });
  }

  private getList(query: { skipCount: number; maxResultCount: number; sorting?: string }): any {
    const input: InvoiceSearchDto = {
      skipCount: query.skipCount,
      maxResultCount: query.maxResultCount,
      sorting: query.sorting ?? `${this.sortingField} ${this.sortingDirection}`,
      filter: this.searchTerm,
    };
    return this.invoiceService.getList(input);
  }

  search(): void {
    this.page = 1;
    this.list.get();
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.page = 1;
    this.list.get();
  }

  changeSorting(field: string): void {
    if (this.sortingField === field) {
      this.sortingDirection = this.sortingDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortingField = field;
      this.sortingDirection = 'asc';
    }
    this.list.get();
  }

  changePage(page: number): void {
    this.page = page;
    this.list.get();
  }

  view(id: string): void {
    this.router.navigate(['/invoices', id]);
  }

  create(): void {
    this.router.navigate(['/invoices/create']);
  }

  edit(id: string): void {
    this.router.navigate(['/invoices', id, 'edit']);
  }

  delete(invoice: InvoiceDto): void {
    this.router.navigate(['/invoices', invoice.id, 'delete']);
  }

  selectInvoice(invoiceId: string): void {
    this.selectedInvoiceId = invoiceId;
  }

  markAsPaid(invoice: InvoiceDto): void {
    debugger
    if (!invoice || invoice.paidUp) return;

    this.invoiceService.markAsPaid(invoice.id).subscribe({
      next: (updated) => {
        invoice.paidUp = updated.paidUp;
      },
      error: (err) => {
        console.error('Failed to mark invoice as paid:', err);
      }
    });
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  get startItem(): number {
    return (this.page - 1) * this.pageSize + 1;
  }

  get endItem(): number {
    return Math.min(this.page * this.pageSize, this.totalCount);
  }
}
