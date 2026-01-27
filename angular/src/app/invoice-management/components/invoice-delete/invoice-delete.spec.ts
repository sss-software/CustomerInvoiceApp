import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceDelete } from './invoice-delete';

describe('InvoiceDelete', () => {
  let component: InvoiceDelete;
  let fixture: ComponentFixture<InvoiceDelete>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceDelete]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceDelete);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
