import { Component } from "@angular/core";
import { CaCertificatesComponent } from "../../components/ca-certificates/ca-certificates.component";
import { ScpCertificatesComponent } from "../../components/scp-certificates/scp-certificates.component";

@Component({
  selector: 'app-certificates',
  imports: [ScpCertificatesComponent, CaCertificatesComponent],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css'
})

export class CertificatesComponent {

}
