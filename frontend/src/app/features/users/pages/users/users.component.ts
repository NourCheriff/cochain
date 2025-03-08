import { Component,OnInit  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-users',
  imports: [],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  companyId: string | null = null;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id');//to get SCP/CA name for query
  }
}
