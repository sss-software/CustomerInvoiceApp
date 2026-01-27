import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CustomerDto } from 'src/app/proxy/customer-management/dtos';
import { CustomerProxyService } from '../../services/customer-proxy-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.scss'
})
export class CustomerDetailsComponent implements OnInit {

  customer?: CustomerDto;
  isLoading = true;

  constructor(
  ) { }

  private route: ActivatedRoute = inject(ActivatedRoute);
  private customerService: CustomerProxyService = inject(CustomerProxyService);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.customerService.get(id).subscribe(result => {
      this.customer = result;
      this.isLoading = false;
    });
  }

  get billingAddress(): string {
    if (!this.customer?.billingAddress) return '';
    const a = this.customer.billingAddress;
    return `${a.street}, ${a.city}, ${a.postalCode}`;
  }
}
