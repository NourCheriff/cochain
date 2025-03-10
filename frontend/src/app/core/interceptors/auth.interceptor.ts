import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let user = this.authService.user;
    if(user && user.access_token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer: ${user.access_token}`,
        }
      });
    }

    return next.handle(request);
  }
}
