import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/pages/login/login.component';

export const routes: Routes = [
  {
    path: 'login',
    title: 'Login',
    component: LoginComponent,
  }
];
