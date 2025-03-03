import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';
import { ContractsComponent } from './features/contracts/pages/contracts/contracts.component';
import { CertificatesComponent } from './features/certificates/pages/certificates/certificates.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
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
  },
  {
    path: 'certificates',
    title: 'Certificates',
    component: CertificatesComponent,
  }
];
