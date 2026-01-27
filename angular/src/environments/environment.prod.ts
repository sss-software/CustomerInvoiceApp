import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44380/',
  redirectUri: baseUrl,
  clientId: 'CustomerInvoiceApp_App',
  responseType: 'code',
  scope: 'offline_access CustomerInvoiceApp',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'CustomerInvoiceApp',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44380',
      rootNamespace: 'CustomerInvoiceApp',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
