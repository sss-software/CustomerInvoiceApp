import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () => import('./home/home.component').then(c => c.HomeComponent),
  },
  {
    path: 'customers',
    loadComponent: () =>
      import('./customer-management/components/customer-list/customer-list').then(
        c => c.CustomerListComponent
      ),
  },
  {
    path: 'customers/create',
    loadComponent: () =>
      import('./customer-management/components/customer-create/customer-create')
        .then(c => c.CustomerCreateComponent),
  },
  {
    path: 'customers/:id/edit',
    loadComponent: () =>
      import('./customer-management/components/customer-edit/customer-edit')
        .then(c => c.CustomerEditComponent),
  },
  {
    path: 'customers/:id/delete',
    loadComponent: () =>
      import('./customer-management/components/customer-delete/customer-delete')
        .then(c => c.CustomerDeleteComponent),
  },
  {
    path: 'customers/:id',
    loadComponent: () =>
      import('./customer-management/components/customer-details/customer-details')
        .then(c => c.CustomerDetailsComponent),
  },
  {
    path: 'invoices',
    loadComponent: () =>
      import('./invoice-management/components/invoice-list/invoice-list')
        .then(c => c.InvoiceListComponent),
  },
  {
    path: 'invoices/create',
    loadComponent: () =>
      import('./invoice-management/components/invoice-create/invoice-create')
        .then(c => c.InvoiceCreateComponent),
  },
  {
    path: 'invoices/:id/edit',
    loadComponent: () =>
      import('./invoice-management/components/invoice-edit/invoice-edit')
        .then(c => c.InvoiceEditComponent),
  },
  {
    path: 'invoices/:id/delete',
    loadComponent: () =>
      import('./invoice-management/components/invoice-delete/invoice-delete')
        .then(c => c.InvoiceDeleteComponent),
  },
  {
    path: 'invoices/:id',
    loadComponent: () =>
      import('./invoice-management/components/invoice-details/invoice-details')
        .then(c => c.InvoiceDetailsComponent),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(c => c.createRoutes()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(c => c.createRoutes()),
  },
  {
    path: 'tenant-management',
    loadChildren: () => import('@abp/ng.tenant-management').then(c => c.createRoutes()),
  },
  {
    path: 'setting-management',
    loadChildren: () => import('@abp/ng.setting-management').then(c => c.createRoutes()),
  },
];
