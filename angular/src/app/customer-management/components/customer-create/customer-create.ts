import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { CustomerProxyService } from '../../services/customer-proxy-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-create',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-create.html',
  styleUrl: './customer-create.scss'
})
export class CustomerCreateComponent {
form: FormGroup;
  isSaving = false;

  private fb = inject(FormBuilder);
  private customerService = inject(CustomerProxyService);
  private router = inject(Router);

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      billingAddress: this.fb.group({
        street: [''],
        city: [''],
        postalCode: [''],
      }),
    });
  }

  save(): void {
     debugger; 
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSaving = true;

   this.customerService.create(this.form.value).subscribe({
      next: () => {
        this.router.navigate(['/customers']);
      },
      error: () => {
        this.isSaving = false;
      },
      complete: () => {
        this.isSaving = false;
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/customers']);
  }
}
