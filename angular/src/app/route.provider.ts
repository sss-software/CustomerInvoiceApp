import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    {
      path: '/customers',
      name: 'Customers',
      iconClass: 'fas fa-users',
      order: 2,
      layout: eLayoutType.application,
      requiredPolicy: 'CustomerManagement.Customers',
    },
     {
      path: '/invoices',
      name: 'Invoices',
      iconClass: 'fas fa-file-invoice',
      order: 3,
      layout: eLayoutType.application,
      requiredPolicy: 'InvoiceManagement.Invoices',
    },
  ]);
}
