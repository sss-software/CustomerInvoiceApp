import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDto } from '@proxy/customer-management/dtos';
import { CustomerProxyService } from '../../services/customer-proxy-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-delete',
  imports: [CommonModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './customer-delete.html',
  styleUrl: './customer-delete.scss'
})
export class CustomerDeleteComponent implements OnInit {
  customer!: CustomerDto;
  form!: FormGroup;
  isDeleting = false;

  private fb = inject(FormBuilder);
  private customerService = inject(CustomerProxyService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  ngOnInit(): void {
    // Create the confirmation checkbox form
    this.form = this.fb.group({
      confirmDelete: [false],
    });

    // Load customer data by ID
    const id = this.route.snapshot.paramMap.get('id')!;
    this.customerService.get(id).subscribe((c) => (this.customer = c));

    this.form.get('confirmDelete')?.valueChanges.subscribe(val => {
      console.log('Checkbox value:', val);
    });
  }

  delete(): void {
    debugger;
    if (!this.form.value.confirmDelete) {
      return;
    }

    this.isDeleting = true;
    this.customerService.delete(this.customer.id!).subscribe({
      next: () => this.router.navigate(['/customers']),
      error: () => (this.isDeleting = false),
      complete: () => (this.isDeleting = false),
    });
  }
}
