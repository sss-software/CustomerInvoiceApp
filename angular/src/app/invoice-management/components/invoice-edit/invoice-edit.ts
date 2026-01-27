import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoiceDto, InvoiceLineDto } from '@proxy/invoice-management/dtos';
import { InvoiceProxyService } from '../../services/invoice-proxy-service';
import { CustomerProxyService } from 'src/app/customer-management/services/customer-proxy-service';
import { CustomerDto } from '@proxy/customer-management/dtos';

@Component({
  selector: 'app-invoice-edit',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './invoice-edit.html',
  styleUrl: './invoice-edit.scss'
})
export class InvoiceEditComponent implements OnInit {
  invoice?: InvoiceDto;
  customers: CustomerDto[] = [];
  form!: FormGroup;
  isLoading = true;
  isSaving = false;

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private invoiceService = inject(InvoiceProxyService);
  private customerService = inject(CustomerProxyService);
  private fb = inject(FormBuilder);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.loadCustomers();
    this.invoiceService.get(id).subscribe(invoice => {
      this.invoice = invoice;
      this.buildForm(invoice);
      this.isLoading = false;
    });
  }

  private loadCustomers(): void {
    this.customerService.getList({ skipCount: 0, maxResultCount: 1000 }).subscribe(result => {
      this.customers = result.items ?? [];
    });
  }

  buildForm(invoice: InvoiceDto): void {
    this.form = this.fb.group({
      customerId: [invoice.customerId?.toString(), Validators.required],
      number: [invoice.number],
      invoiceDate: [
        invoice.invoiceDate
          ? invoice.invoiceDate.substring(0, 10)
          : '',
        Validators.required
      ],
      lines: this.fb.array([])
    });

    invoice.lines.forEach(line => this.addLine(line));
  }

  get lines(): FormArray {
    return this.form.get('lines') as FormArray;
  }

  addLine(line?: InvoiceLineDto) {
    this.lines.push(
      this.fb.group({
        id: [line?.id ?? null], 
        productId: [line?.productId || this.generateRandomGuid(), Validators.required],
        description: [line?.description || '', Validators.required],
        quantity: [line?.quantity || 1, [Validators.required, Validators.min(1)]],
        unitPrice: [line?.unitPrice || 0, [Validators.required, Validators.min(0)]]
      })
    );
  }

  removeLine(index: number) {
    this.lines.removeAt(index);
  }

  generateRandomGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
      const r = (Math.random() * 16) | 0,
        v = c === 'x' ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      console.log('Form is invalid:', this.form.value);
      return;
    }

    this.isSaving = true;

    const input = this.form.value;
    this.invoiceService.update(this.invoice!.id, input).subscribe({
      next: () => {
        this.isSaving = false;
        this.router.navigate(['/invoices']);
      },
      error: (err) => {
        this.isSaving = false;
        console.error('Error updating invoice:', err);
      }
    });
  }

  get totalAmount(): number {
    return this.lines.controls.reduce((acc, line) => {
      const l = line.value;
      return acc + (l.quantity * l.unitPrice);
    }, 0);
  }

  cancel(): void {
    this.router.navigate(['/invoices']);
  }
}
