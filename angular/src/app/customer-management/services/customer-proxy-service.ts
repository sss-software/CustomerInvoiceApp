import { PagedResultDto } from '@abp/ng.core';
import { inject, Injectable } from '@angular/core';
import { CustomerDto, CustomerSearchDto } from '@proxy/customer-management/dtos';
import { Observable } from 'rxjs';
import { CustomerService } from 'src/app/proxy/customer-management';

@Injectable({
  providedIn: 'root'
})
@Injectable({
  providedIn: 'root',
})
export class CustomerProxyService {
  private api = inject(CustomerService);

  getList(input: CustomerSearchDto): Observable<PagedResultDto<CustomerDto>> {
    return this.api.getList(input);
  }

  get(id: string) {
    return this.api.get(id);
  }

  create(input: any) {
    return this.api.create(input);
  }

  update(id: string, input: any) {
    return this.api.update(id, input);
  }

  delete(id: string) {
    return this.api.delete(id);
  }
}