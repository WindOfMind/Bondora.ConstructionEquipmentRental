import { EnvironmentSettings } from './environment.interface';

export const environment: EnvironmentSettings = {
  production: false,
  equipmentApiEndpoint: 'http://localhost:5000/api/equipment',
  invoiceApiEndpoint: 'http://localhost:5000/api/invoices',
  invoiceHubEndpoint: 'http://localhost:5000/invoiceshub'
};
