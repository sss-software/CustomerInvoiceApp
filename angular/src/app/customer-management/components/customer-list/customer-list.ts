import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { ListService } from '@abp/ng.core';
import { CustomerProxyService } from '../../services/customer-proxy-service';
import { CustomerDto, CustomerSearchDto } from '@proxy/customer-management/dtos/models';
import { ModalRefService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  providers: [ListService],
  templateUrl: './customer-list.html',
  styleUrls: ['./customer-list.scss'],
})
export class CustomerListComponent implements OnInit {
  customers: CustomerDto[] = [];
  totalCount = 0;
  isCustomerModalVisible = false;

  selectedCustomerId: string | null = null;

  selectCustomer(customerId: string): void {
    this.selectedCustomerId = customerId;
  }

  // Search + sorting + paging state
  searchTerm = '';
  sortingField = 'name';
  sortingDirection: 'asc' | 'desc' = 'asc';
  page = 1;
  pageSize = 10;

  constructor(
  ) { }

  public readonly list: ListService = inject(ListService);
  private customerService = inject(CustomerProxyService);
  private router: Router = inject(Router);
  private modalService = inject(ModalRefService);
  isModalOpen: boolean = false;
  ngOnInit(): void {
    // Connect ListService to proxy getList
    this.list.hookToQuery((query) => this.getList(query)).subscribe((result) => {
      this.customers = result.items ?? [];
      this.totalCount = result.totalCount;
    });
  }

  private getList(query: {
    skipCount: number;
    maxResultCount: number;
    sorting?: string;
  }): any {
    const input: CustomerSearchDto = {
      skipCount: query.skipCount,
      maxResultCount: query.maxResultCount,
      sorting: query.sorting ?? `${this.sortingField} ${this.sortingDirection}`,
      filter: this.searchTerm,
    };
    return this.customerService.getList(input);
  }

  search(): void {
    this.page = 1;

    if (!this.searchTerm || this.searchTerm.trim() === '') {
      this.list.get();
      return;
    }

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
    this.router.navigate(['/customers', id]);
  }

  create(): void {
    this.router.navigate(['/customers/create']);
  }

  edit(id: string): void {
    this.router.navigate(['/customers', id, 'edit']);
  }

  delete(customer: CustomerDto): void {
    this.router.navigate(['/customers', customer.id, 'delete']);
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

