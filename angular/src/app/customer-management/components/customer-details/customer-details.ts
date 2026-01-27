import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CustomerDto } from 'src/app/proxy/customer-management/dtos';
import { CustomerProxyService } from '../../services/customer-proxy-service';
import { CommonModule } from '@angular/common';
import { InvoiceProxyService } from 'src/app/invoice-management/services/invoice-proxy-service';
import { InvoiceDto } from '@proxy/invoice-management/dtos/models';

@Component({
  selector: 'app-customer-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.scss'
})
export class CustomerDetailsComponent implements OnInit {

  customer?: CustomerDto;
  invoices: InvoiceDto[] = [];
  isLoading = true;

  constructor(
  ) { }

  private route: ActivatedRoute = inject(ActivatedRoute);
  private customerService: CustomerProxyService = inject(CustomerProxyService);
  private invoiceService: InvoiceProxyService = inject(InvoiceProxyService);
  private router = inject(Router);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.customerService.get(id).subscribe(result => {
      this.customer = result;
      this.invoiceService.getByCustomer(id).subscribe(invoices => {
        this.invoices = invoices;
        this.isLoading = false;
      });

      this.isLoading = false;
    });
  }

  get billingAddress(): string {
    if (!this.customer?.billingAddress) return '';
    const a = this.customer.billingAddress;
    return `${a.street}, ${a.city}, ${a.postalCode}`;
  }

   viewInvoice(id: string): void {
    this.router.navigate(['/invoices', id]);
  }

  editInvoice(id: string): void {
    this.router.navigate(['/invoices', id, 'edit']);
  }

  getInvoiceTotal(invoice: InvoiceDto): number {
    return (
      invoice.lines?.reduce(
        (sum, l) => sum + l.quantity * l.unitPrice,
        0
      ) ?? 0
    );
  }
}
