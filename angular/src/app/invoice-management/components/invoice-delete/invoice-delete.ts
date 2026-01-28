import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { InvoiceDto } from '@proxy/invoice-management/dtos';
import { InvoiceProxyService } from '../../services/invoice-proxy-service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CustomerProxyService } from 'src/app/customer-management/services/customer-proxy-service';

@Component({
  selector: 'app-invoice-delete',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  standalone: true,
  templateUrl: './invoice-delete.html',
  styleUrl: './invoice-delete.scss'
})
export class InvoiceDeleteComponent implements OnInit {

  invoice?: InvoiceDto;
  isLoading = true;
  totalInvoiceAmount = 0;
  form!: FormGroup;
  isDeleting = false;

  private fb = inject(FormBuilder);
  private route: ActivatedRoute = inject(ActivatedRoute);
  private invoiceService: InvoiceProxyService = inject(InvoiceProxyService);
  private router = inject(Router);

  constructor(
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      confirmDeactivation: [false],
    });
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

    this.form.get('confirmDeactivation')?.valueChanges.subscribe(val => {
      console.log('Checkbox value:', val);
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

  deactivate(): void {
    if (!this.form.value.confirmDeactivation) {
      return;
    }

    this.isDeleting = true;
    this.invoiceService.delete(this.invoice!.id!).subscribe({
      next: () => this.router.navigate(['/invoices']),
      error: () => (this.isDeleting = false),
      complete: () => (this.isDeleting = false),
    });
  }
}
