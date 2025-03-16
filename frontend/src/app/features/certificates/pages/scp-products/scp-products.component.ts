import {AfterViewInit, Component, OnInit, ViewChild,inject} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog } from '@angular/material/dialog';
import { FileInputComponent } from '../../components/file-input/file-input.component';
import {MatSort, MatSortModule} from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ScpProductsService } from '../../service/scp-products.service';
@Component({
  selector: 'app-scp-products',
  imports: [CommonModule,MatSortModule,MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './scp-products.component.html',
  styleUrl: './scp-products.component.css'
})
export class ScpProductsComponent implements AfterViewInit, OnInit {

  readonly dialog = inject(MatDialog);

  private route = inject(ActivatedRoute);
  private scpProductsService = inject(ScpProductsService);

  scpType: SCPType = {
      "type": "SCP"
  }

  displayedColumns: string[] = ['name', 'category', 'expirationDate', 'attachments'];
  dataSource = new MatTableDataSource<SCPProducts>(scpProducts);
  certificateId: number | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.getScpProducts()
  }

  getScpProducts(){
    const id = this.route.snapshot.paramMap.get('id')!;
    this.scpProductsService.getScpProducts(id).subscribe({
      next: (response) => { console.log(response) },
      error: (error) => { console.log(error) }
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  attachCertificate() {
    this.dialog.open(FileInputComponent);
  }
}

export interface SCPType {
  type: string
}

export interface SCPProducts {
  name: string;
  category: string;
  expirationDate: string;
}


const scpProducts: SCPProducts[] = [
  {
    'name':'ProductA',
    'category': 'CategoryA',
    'expirationDate':'17-04-2025'
  },
  {
    'name':'ProductB',
    'category': 'CategoryC',
    'expirationDate':'17-04-2025'
  },
  {
    'name':'ProductB',
    'category': 'CategoryA',
    'expirationDate':'15-04-2025'
  },
  {
    'name':'ProductC',
    'category': 'CategoryC',
    'expirationDate':'17-03-2025'
  },
  {
    'name':'ProductD',
    'category': 'CategoryD',
    'expirationDate':'01-08-2025'
  },
  {
    'name':'ProductA',
    'category': 'CategoryB',
    'expirationDate':'03-04-2025'
  },

]
