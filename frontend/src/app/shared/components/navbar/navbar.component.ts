import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { AuthService } from 'src/app/core/services/auth.service';
@Component({
  selector: 'app-navbar',
  imports: [BreadcrumbComponent,MatIconModule, MatDividerModule, MatButtonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  private router = inject(Router);
  private authService = inject(AuthService)

  isWalletRoute(): boolean {
    return this.router.url === '/';
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}
