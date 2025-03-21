import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
@Component({
  selector: 'app-navbar',
  imports: [BreadcrumbComponent,MatIconModule, MatDividerModule, MatButtonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  private router = inject(Router);
  private authService = inject(AuthService)

  username = this.authService.username;
  userRole: Role = this.authService.userRole!;

  private pagePermissions: { [key: string]: Role[] } = {
    wallet: [Role.AdminSCP, Role.UserSCP],
    products: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
    certificates: [Role.AdminCA, Role.UserCA, Role.AdminSCP, Role.UserSCP],
    contracts: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
    companies: [Role.SysAdmin],
    logs: [Role.SysAdmin]
  };

  hasAccess(page: string): boolean {
    return this.pagePermissions[page]?.includes(this.userRole) ?? false;
  }

  isWalletRoute(): boolean {
    return this.router.url === '/';
  }

  logout(): void {
    this.authService.logout();
  }
}
