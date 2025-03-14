import { Component, inject, OnInit } from '@angular/core';
import { WelcomeComponent } from "../../components/welcome/welcome.component";
import { LoginSectionComponent } from "../../components/login-section/login-section.component";
import { AuthService } from 'src/app/core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [WelcomeComponent, LoginSectionComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  ngOnInit(): void {
    if (this.authService.token)
      this.router.navigateByUrl('');
  }

}
