import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ContractDialogComponent } from '../contract-dialog/contract-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ContractService } from '../../service/contract.service';
import { Contract } from 'src/models/documents/contract.model';
import { CommonModule } from '@angular/common';
import { DefaultPagination } from 'src/app/core/utilities/pagination-response';
import { AuthService } from 'src/app/core/services/auth.service';
import { Role } from 'src/types/roles.enum';
import { DocumentType } from 'src/types/document.enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-contracts-section',
  templateUrl: './contracts-section.component.html',
  styleUrl: './contracts-section.component.css',
  imports: [CommonModule,MatTableModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatSelectModule]
})
export class ContractsSectionComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  displayedColumns: string[] = ['emitter', 'receiver', 'workType', 'attachment'];

  private authService = inject(AuthService)
  private contractService = inject(ContractService)
  private toasterService = inject(ToastrService)

  dataSource = new MatTableDataSource<Contract>([]);
  totalRecords = 0;

  selected = 'received_contracts';
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  ngOnInit() {
    if (this.isAdmin()) {
      this.displayedColumns.push('action');
    }
    this.getContracts()
    this.dataSource.paginator = this.paginator;
  }

  onPageChange(event: PageEvent){
    this.getContracts(event.pageSize, event.pageIndex)
  }

  getContracts(pageSize: number = DefaultPagination.defaultPageSize, pageNumber: number = DefaultPagination.defaultPageNumber){
    const currentSCPId = this.authService.userId!
    this.contractService.getContracts(currentSCPId,this.selected,pageSize.toString(),pageNumber.toString()).subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource<Contract>(response.items!);
        this.totalRecords = response.totalSize;
      },
      error: (error) => console.log(error)
    })
  }

  updateSelected(event: any){
    this.selected = event.value
    this.getContracts()
  }

  isAdmin(): boolean {
    return this.authService.userRoles!.includes(Role.SysAdmin);
  }

  deleteContract(id: string){
    this.contractService.deleteContract(id, DocumentType.Contract).subscribe({
      next: (response) => {
        this.toasterService.info(`Removed contract ${response.name}`, 'Info');
        this.getContracts()
      },
      error: (error) => { console.log(error) }
    })
  }

  addContractDialog() {
    this.dialog.open(ContractDialogComponent);
  }

}


