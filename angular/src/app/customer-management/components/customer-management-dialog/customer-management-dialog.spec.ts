import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerManagementDialog } from './customer-management-dialog';

describe('CustomerManagementDialog', () => {
  let component: CustomerManagementDialog;
  let fixture: ComponentFixture<CustomerManagementDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerManagementDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerManagementDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
