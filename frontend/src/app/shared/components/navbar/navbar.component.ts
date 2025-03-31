import { Component, inject, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
import { User } from 'src/models/auth/user.model';
import { CompanyType } from 'src/types/company.enum';
@Component({
  selector: 'app-navbar',
  imports: [BreadcrumbComponent,MatIconModule, MatDividerModule, MatButtonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  private router = inject(Router);
  private authService = inject(AuthService)
  currentUser!: User;
  selected!: CompanyType;
  url!: string;

  ngOnInit(): void {
    this.authService.getUser().subscribe(result => {
      this.currentUser = result;
      this.url = "companies/" + this.currentUser.supplyChainPartnerId + "/users";
      if(this.authService.userRoles!.includes(Role.AdminSCP)){
        this.selected = CompanyType.SupplyChainPartner;
      }
      else{
        this.selected = CompanyType.CertificationAuthority;
      }
    });
  }




  username = this.authService.username;
  userRoles: Role[] = this.authService.userRoles!;

  private pagePermissions: { [key: string]: Role[] } = {
    wallet: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
    products: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
    certificates: [Role.SysAdmin, Role.AdminCA, Role.UserCA, Role.AdminSCP, Role.UserSCP],
    contracts: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
    companies: [Role.SysAdmin],
    users: [Role.AdminSCP, Role.AdminCA],
    logs: [Role.SysAdmin],
    offsettingActions: [Role.SysAdmin, Role.AdminSCP, Role.UserSCP],
  };

  hasAccess(page: string): boolean {
    return this.userRoles.some(role => this.pagePermissions[page].includes(role));
  }

  isWalletRoute(): boolean {
    return this.router.url === '/';
  }

  logout(): void {
    this.authService.logout();
  }
}
