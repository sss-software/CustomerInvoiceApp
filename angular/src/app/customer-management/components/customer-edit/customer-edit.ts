import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerProxyService } from '../../services/customer-proxy-service';

@Component({
  selector: 'app-customer-edit',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-edit.html',
  styleUrl: './customer-edit.scss'
})
export class CustomerEditComponent implements OnInit {
 isSaving = false;
  customerId!: string;

  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private customerService = inject(CustomerProxyService);

  form = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: [''],
    billingAddress: this.fb.group({
      street: [''],
      city: [''],
      postalCode: [''],
    }),
  });

  ngOnInit(): void {
    this.customerId = this.route.snapshot.paramMap.get('id')!;
    this.loadCustomer();
  }

  private loadCustomer(): void {
    this.customerService.get(this.customerId).subscribe(customer => {
      this.form.patchValue(customer);
    });
  }

  save(): void {
    debugger;
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSaving = true;

    this.customerService.update(this.customerId, this.form.value).subscribe({
      next: () => {
        this.router.navigate(['/customers', this.customerId]);
      },
      error: () => (this.isSaving = false),
      complete: () => (this.isSaving = false),
    });
  }

  cancel(): void {
    this.router.navigate(['/customers', this.customerId]);
  }
}
