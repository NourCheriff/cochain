import { RouterLink, Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';
import { ContractsComponent } from './features/contracts/pages/contracts/contracts.component';
import { CertificatesComponent } from './features/certificates/pages/certificates/certificates.component';
import { ScpProductsComponent } from './features/certificates/pages/scp-products/scp-products.component';
import { ProductsComponent } from './features/products/pages/products/products.component';
import { CompaniesComponent } from './features/users/pages/companies/companies.component';
import { UsersComponent } from './features/users/pages/users/users.component';

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
  },
  {
    path: 'products',
    title: 'Products',
    component: ProductsComponent,
    data: { breadcrumb: 'Products' }
  },
  {
    path: 'companies',
    title: 'Companies',
    component: CompaniesComponent,
    data: { breadcrumb: 'Companies' }
  },
  {
    path: 'companies/users/:id',
    title: 'Users',
    component: UsersComponent,
    data: { breadcrumb: 'Users' }
},
];
