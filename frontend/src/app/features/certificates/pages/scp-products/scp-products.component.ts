import {Component, OnInit, ViewChild,inject} from '@angular/core';
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
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
import { CertificatesService } from '../../service/certificates.service';
import { ProductInfo } from 'src/models/product/product-info.model';
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';
@Component({
  selector: 'app-scp-products',
  imports: [CommonModule,MatSortModule,MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './scp-products.component.html',
  styleUrl: './scp-products.component.css'
})
export class ScpProductsComponent implements OnInit {

  readonly dialog = inject(MatDialog);

  private route = inject(ActivatedRoute);
  private certificateService = inject(CertificatesService);

  scpType: SCPType = {
      "type": "CA"
  }

  displayedColumns: string[] = ['name', 'category', 'expirationDate', 'attachments'];
  dataSource = new MatTableDataSource<ProductInfo>([]);
  totalRecord = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getScpProducts()
    this.dataSource.paginator = this.paginator;
  }

  getScpProducts(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber){
    const id = this.route.snapshot.paramMap.get('id')!;
    this.certificateService.getScpProducts(id,pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource<ProductInfo>(response.items!)
        this.totalRecord = response.totalSize;
        this.dataSource.sort = this.sort;
      },
      error: (error) => { console.log(error) }
    })
  }

  applyFilter() {
      //api call queryString
  }

  deleteCertificate(id: string){
    this.certificateService.deleteCertificate(id).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => { console.log(error) }
    })
  }

  attachCertificate(scpReceiverId: string) {
    this.dialog.open(FileInputComponent,{
      data: {
        scpReceiverId: scpReceiverId,
        documentType: 'quality'
      }
    });
  }

  onPageChange(event: PageEvent){
    this.getScpProducts(event.pageSize, event.pageIndex)
  }
}

export interface SCPType {
  type: string
}
