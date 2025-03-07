import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
@Component({
  selector: 'app-navbar',
  imports: [BreadcrumbComponent,MatIconModule, MatDividerModule, MatButtonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  readonly router = inject(Router);

  isWalletRoute(): boolean {
    console.log(this.router.url);
    return this.router.url === '/';
  }
}
