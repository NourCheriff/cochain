import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';
import { ContractsComponent } from './features/contracts/pages/contracts/contracts.component';
import { CertificatesComponent } from './features/certificates/pages/certificates/certificates.component';
import { WalletComponent } from './features/wallet/pages/wallet/wallet.component';
import { ScpProductsComponent } from './features/certificates/pages/scp-products/scp-products.component';
import { ProductsComponent } from './features/products/pages/products/products.component';
import { ProductDetailsComponent } from './features/products/pages/product-details/product-details.component';

export const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: WalletComponent
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
    data: { breadcrumb: 'Contracts' }
  },
  {
    path: 'certificates',
    title: 'Certificates',
    component: CertificatesComponent,
    data: { breadcrumb: 'Certificates' }
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
    path:'products/details',
    title:'Product details',
    component: ProductDetailsComponent,
    data: { breadcrumb: 'Product details' }
  }
];
