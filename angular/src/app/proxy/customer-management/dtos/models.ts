import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AddressDto {
  street?: string;
  city?: string;
  postalCode?: string;
}

export interface CreateUpdateCustomerDto {
  name?: string;
  email?: string;
  phone?: string;
  billingAddress?: AddressDto;
}

export interface CustomerDto extends EntityDto<string> {
  name?: string;
  email?: string;
  phone?: string;
  billingAddress?: AddressDto;
}

export interface CustomerSearchDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
}
