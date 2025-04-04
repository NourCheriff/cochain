import { Component, inject, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { filter, distinctUntilChanged } from 'rxjs/operators';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import "primeicons/primeicons.css";
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css'],
  standalone: true,
  imports: [BreadcrumbModule]
})
export class BreadcrumbComponent implements OnInit {

  private authService = inject(AuthService)

  items: MenuItem[] = [];
  userRoles = this.authService.userRoles!;

  home: MenuItem = {
    label: 'Wallet',
    icon: 'pi pi-home',
    separator: true,
    iconStyle:{
      'margin-right':'5px'
    },
    routerLink: '/',
    style:{
      'color':'var(--breadcrumb-link)',
      'margin-right':'5px'
    }
  }

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    if (this.userRoles.some(role => role === Role.UserCA || role === Role.AdminCA)) {
      this.home.routerLink = '/certificates';
      this.home.label = 'Certificates';
    }
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.items = this.createBreadcrumb(this.activatedRoute.root);
        this.items
      });
  }

  private createBreadcrumb(
    route: ActivatedRoute,
    url: string = '',
    breadcrumbs: MenuItem[] = []
  ): MenuItem[] {

    const children: ActivatedRoute[] = route.children;
    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {

      const routeURL: string = child.snapshot.url.map(segment => segment.path).join('/');
      if (routeURL) {
        url += `/${routeURL}`;
      }


      const label = child.snapshot.data['breadcrumb'];
      if (label) {
        let id = child.snapshot.paramMap.get('id');
        switch(label){
          case 'Details':
            breadcrumbs.push(
              {
                label: 'Certificates',
                routerLink: '/certificates',
                style:{
                  'color':'var(--breadcrumb-link)',
                  'margin-left':'5px',
                  'margin-right':'5px'
                }
              },
              {
                label: `Details`,
                routerLink: `/details/${id}`,
                style:{'margin-left':'5px'}
              }
            );
            break;
          case 'Product details':
            breadcrumbs.push(
              {
                label: 'Product',
                routerLink: '/products',
                style:{
                  'color':'var(--breadcrumb-link)',
                  'margin-left':'5px',
                  'margin-right':'5px'
                }
              },
              {
                label: `Details`,
                routerLink: `/details/${id}`,
                style:{'margin-left':'5px'}
              }
            );
            break;
          case 'Companies Users':
            breadcrumbs.push(
              {
                label: 'Companies',
                routerLink: '/companies',
                style:{
                  'color':'var(--breadcrumb-link)',
                  'margin-left':'5px',
                  'margin-right':'5px'
                }
              },
              {
                label: `Users`,
                routerLink: `/${id}/users`,
                style:{'margin-left':'5px'}
              }
            );
            break;

          default:
            // Crea un singolo menu item per altri casi
            let menuItem: MenuItem = { label: label };
            // Se la rotta specifica 'routerLink' nel data, usalo, altrimenti utilizza l'URL accumulato
            menuItem.routerLink = child.snapshot.data['routerLink'] || url;
            breadcrumbs.push(menuItem);
          break;
        }
    }

      // Continua a scorrere ricorsivamente
      return this.createBreadcrumb(child, url, breadcrumbs);
    }

    return breadcrumbs;
  }
}
