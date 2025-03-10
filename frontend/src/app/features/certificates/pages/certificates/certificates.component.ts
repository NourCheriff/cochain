import { Component } from "@angular/core";
import { CaCertificatesComponent } from "../../components/ca-certificates/ca-certificates.component";

@Component({
  selector: 'app-certificates',
  imports: [CaCertificatesComponent],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css'
})

export class CertificatesComponent {

}
