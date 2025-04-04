import {AfterViewInit, Component, OnInit, ViewChild,inject} from '@angular/core';
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
import { ToastrService } from 'ngx-toastr';
import { DocumentType } from 'src/types/document.enum';
import { SupplyChainPartnerCertificate } from 'src/models/documents/supply-chain-partner-certificate.model';
import { Role } from 'src/types/roles.enum';
import { AuthService } from 'src/app/core/services/auth.service';
import { ProductDocument } from 'src/models/documents/product-document.model';
@Component({
  selector: 'app-scp-products',
  imports: [CommonModule,MatSortModule,MatInputModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './scp-products.component.html',
  styleUrl: './scp-products.component.css'
})
export class ScpProductsComponent implements OnInit, AfterViewInit {

  readonly dialog = inject(MatDialog);

  private route = inject(ActivatedRoute);
  private authService = inject(AuthService)
  private certificateService = inject(CertificatesService);
  private toastrService =  inject(ToastrService);

  displayedColumns: string[] = ['name', 'category', 'expirationDate'];
  dataSource = new MatTableDataSource<ProductInfo>([]);
  totalRecord = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getScpProducts()
    this.dataSource.paginator = this.paginator;
  }

  ngAfterViewInit(): void {
    this.displayedColumns.push('attachments')
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

  deleteCertificate(id: string){
    this.certificateService.deleteQualityCertificate(id).subscribe({
      next: () => {
        this.toastrService.info(`Removed Quality certificate`, 'info')
        this.getScpProducts();
      },
      error: (error) => { console.log(error) }
    })
  }

  attachCertificate(product: ProductInfo) {
    const dialogRef = this.dialog.open(FileInputComponent,{
      data: {
        scpReceiverId: product.supplyChainPartnerId,
        documentType: DocumentType.Quality,
        productInfoId: product.id
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
       this.getScpProducts()
    });
  }


  isAuthorizated(): boolean {
    const roles = [Role.SysAdmin, Role.UserCA, Role.AdminCA];
    return this.authService.userRoles?.some(role => roles.includes(role)) ?? false;
  }

  getQualityCertificate(productDocuments: ProductDocument[]): ProductDocument | null {
    console.log(productDocuments)
    if (!productDocuments?.length) return null;

    return productDocuments.find(doc => doc.type === DocumentType.Quality) || null;
  }

  onPageChange(event: PageEvent){
    this.getScpProducts(event.pageSize, event.pageIndex)
  }
}
