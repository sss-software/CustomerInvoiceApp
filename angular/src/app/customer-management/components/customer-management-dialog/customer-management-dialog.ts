import { Component, effect, EventEmitter, inject, Input, OnInit, Output, WritableSignal } from '@angular/core';
import { CustomerProxyService } from '../../services/customer-proxy-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ModalComponent, ModalCloseDirective } from '@abp/ng.theme.shared'


@Component({
  selector: 'app-customer-management-dialog',
  imports: [CommonModule,
    ReactiveFormsModule,
    ModalComponent,
    ModalCloseDirective],
  standalone: true,
  templateUrl: './customer-management-dialog.html',
  styleUrl: './customer-management-dialog.scss'
})
export class CustomerManagementDialog implements OnInit {
  form: FormGroup;
  isSaving = false;

  @Input() visible = false; 
  @Output() visibleChange = new EventEmitter<boolean>();
  isModalOpen = false;

  private customerService: CustomerProxyService = inject(CustomerProxyService);
  private fb: FormBuilder = inject(FormBuilder);
  constructor() { }

  ngOnInit(): void {
    this.buildForm();
    // effect(() => {
    //   console.log('[Modal] signal changed:', this.visibleSignal());
    //   this.modalVisible = this.visibleSignal();
    // });
  }

  private buildForm(): void {
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
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSaving = true;

    this.customerService.create(this.form.value).subscribe({
      next: () => {
        this.close();
        this.isSaving = false;
      },
      error: () => (this.isSaving = false),
      complete: () => (this.isSaving = false),
    });
  }
close(): void { 
  this.visible = false; 
  this.visibleChange.emit(false); }

}
