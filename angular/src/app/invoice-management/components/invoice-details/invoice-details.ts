import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { InvoiceDto } from '@proxy/invoice-management/dtos';
import { InvoiceProxyService } from '../../services/invoice-proxy-service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-invoice-details',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  standalone: true,
  templateUrl: './invoice-details.html',
  styleUrls: ['./invoice-details.scss'],
})
export class InvoiceDetailsComponent implements OnInit {

  invoice?: InvoiceDto;
  isLoading = true;
  totalInvoiceAmount = 0; 

  private route: ActivatedRoute = inject(ActivatedRoute);
  private invoiceService: InvoiceProxyService = inject(InvoiceProxyService);

  constructor(
  ) { }

ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.invoiceService.get(id).subscribe({
      next: (result) => {
        this.invoice = result;
        this.isLoading = false;

        this.totalInvoiceAmount = this.invoice.lines.reduce(
          (acc, l) => acc + l.quantity * l.unitPrice,
          0
        );

        console.log('Invoice loaded:', this.invoice);
      },
      error: (err) => {
        console.error('Error loading invoice:', err);
        this.isLoading = false;
      }
    });
  }

  get customerName(): string {
    return this.invoice?.customerName ?? '';
  }


  get invoiceDate(): string {
    return this.invoice?.invoiceDate ? new Date(this.invoice.invoiceDate).toLocaleDateString() : '';
  }

  get totalAmount(): number {
    if (!this.invoice?.lines) return 0;
    return this.invoice.lines.reduce((sum, line) => sum + (line.quantity * line.unitPrice), 0);
  }
}
