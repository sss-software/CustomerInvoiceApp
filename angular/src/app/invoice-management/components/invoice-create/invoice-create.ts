import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerDto } from '@proxy/customer-management/dtos';
import { CustomerProxyService } from 'src/app/customer-management/services/customer-proxy-service';
import { InvoiceProxyService } from '../../services/invoice-proxy-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-invoice-create',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './invoice-create.html',
  styleUrl: './invoice-create.scss'
})
export class InvoiceCreateComponent implements OnInit {
  form!: FormGroup;
  customers: CustomerDto[] = [];
  isSaving = false;

  private fb = inject(FormBuilder);
  private invoiceService = inject(InvoiceProxyService);
  private customerService = inject(CustomerProxyService);
  private router = inject(Router);

  ngOnInit(): void {
    this.loadCustomers();
    this.buildForm();
  }

  private loadCustomers(): void {
    this.customerService.getList({ skipCount: 0, maxResultCount: 1000 }).subscribe(result => {
      this.customers = result.items ?? [];
    });
  }

  private buildForm(): void {
    this.form = this.fb.group({
      customerId: [null, Validators.required],
      invoiceDate: [new Date(), Validators.required],
      number: [`INV-${Math.floor(Math.random() * 1000) + 1000}`], // temporary auto-generate number
      lines: this.fb.array([]),
    });

    // Initialize with one line by default
    this.addLine();
  }

  get lines(): FormArray {
    return this.form.get('lines') as FormArray;
  }

  private createLineFormGroup(): FormGroup {
    const group = this.fb.group({
      productId: [''], // will be auto-populated
      description: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
    });

    // Auto-generate productId when description changes
    group.get('description')?.valueChanges.subscribe(desc => {
      if (desc && !group.get('productId')?.value) {
        group.get('productId')?.setValue(this.generateGuid(), { emitEvent: false });
      }
    });

    return group;
  }

  addLine(): void {
    this.lines.push(this.createLineFormGroup());
  }

  removeLine(index: number): void {
    this.lines.removeAt(index);
  }

  save(): void {
    debugger;
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      console.log('Form is invalid. Current errors:');
      this.logFormErrors(this.form);
      return;
    }

    this.isSaving = true;
    this.invoiceService.create(this.form.value).subscribe({
      next: () => {
        this.isSaving = false;
        this.router.navigate(['/invoices']);
      },
      error: () => {
        this.isSaving = false;
      },
    });
  }

  private generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  private logFormErrors(group: FormGroup | FormArray, parentKey: string = ''): void {
    Object.keys(group.controls).forEach(key => {
      const control = group.controls[key];
      const controlKey = parentKey ? `${parentKey}.${key}` : key;

      if (control instanceof FormGroup || control instanceof FormArray) {
        this.logFormErrors(control, controlKey);
      } else if (control.invalid) {
        console.log(`Control "${controlKey}" is invalid:`, control.errors);
      }
    });
  }


  cancel(): void {
    this.router.navigate(['/customers']);
  }
}