import { Component } from '@angular/core';
import { WelcomeComponent } from "../../components/welcome/welcome.component";
import { LoginSectionComponent } from "../../components/login-section/login-section.component";

@Component({
  selector: 'app-login',
  imports: [WelcomeComponent, LoginSectionComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

}
