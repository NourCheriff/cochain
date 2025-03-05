import { RouterLink, Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';
import { ContractsComponent } from './features/contracts/pages/contracts/contracts.component';
import { CertificatesComponent } from './features/certificates/pages/certificates/certificates.component';
import { ScpProductsComponent } from './features/certificates/pages/scp-products/scp-products.component';

export const routes: Routes = [
  {
    path: 'home',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: 'login',
    title: 'Login',
    component: LoginComponent,
  },
  {
    path: 'contracts',
    title: 'Contracts',
    component: ContractsComponent,
    data: { breadcrumb: 'Contracts'}
  },
  {
    path: 'certificates',
    title: 'Certificates',
    component: CertificatesComponent,
    data: { breadcrumb: 'Certificates'}
  },
  {
      path: 'certificates/details/:id',
      title: 'Details',
      component: ScpProductsComponent,
      data: { breadcrumb: 'Details' }
  }
];
