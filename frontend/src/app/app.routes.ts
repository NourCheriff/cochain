import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';
import { ContractsComponent } from './features/contracts/pages/contracts/contracts.component';
import { CertificatesComponent } from './features/certificates/pages/certificates/certificates.component';
import { WalletComponent } from './features/wallet/pages/wallet/wallet.component';
import { ScpProductsComponent } from './features/certificates/pages/scp-products/scp-products.component';
import { ProductsComponent } from './features/products/pages/products/products.component';
import { CompaniesComponent } from './features/users/pages/companies/companies.component';
import { UsersComponent } from './features/users/pages/users/users.component';
import { ProductDetailsComponent } from './features/products/pages/product-details/product-details.component';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';
import { LogsComponent } from './features/logs/pages/logs.component';

export const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: WalletComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    title: 'Login',
    component: LoginComponent
  },
  {
    path: 'contracts',
    title: 'Contracts',
    component: ContractsComponent,
    data: { breadcrumb: 'Contracts' },
    canActivate: [AuthGuard],
  },
  {
    path: 'certificates',
    title: 'Certificates',
    component: CertificatesComponent,
    data: { breadcrumb: 'Certificates' },
    canActivate: [AuthGuard],
  },
  {
      path: 'certificates/details/:id',
      title: 'Details',
      component: ScpProductsComponent,
      data: { breadcrumb: 'Details' },
      canActivate: [AuthGuard],
  },
  {
    path: 'products',
    title: 'Products',
    component: ProductsComponent,
    data: { breadcrumb: 'Products' },
    canActivate: [AuthGuard],
  },
  {
    path: 'companies',
    title: 'Companies',
    component: CompaniesComponent,
    data: { breadcrumb: 'Companies' },
    canActivate: [AuthGuard, RoleGuard],
  },
  {
    path: 'companies/:id/users',
    title: 'Users',
    component: UsersComponent,
    data: { breadcrumb: 'Companies Users' },
    canActivate: [AuthGuard, RoleGuard],
  },
  {
    path:'products/details/:id',
    title:'Product details',
    component: ProductDetailsComponent,
    data: { breadcrumb: 'Product details' },
    canActivate: [AuthGuard],
  },
  {
    path:'logs',
    title:'System Logs',
    component: LogsComponent,
    data: { breadcrumb: 'System Logs' },
    canActivate: [AuthGuard],
  },
  {
    path:'**',
    redirectTo: '',
  }
];
