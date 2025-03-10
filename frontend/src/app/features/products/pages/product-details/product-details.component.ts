import { Component, AfterViewInit, ViewChild, inject } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { NewWorkDialogComponent } from '../../components/new-work-dialog/new-work-dialog.component';
import { EditProductDialogComponent } from '../../components/edit-product-dialog/edit-product-dialog.component';

@Component({
  selector: 'app-product-details',
  imports: [MatTableModule, MatPaginatorModule, MatCardModule,MatButtonModule, MatDividerModule, MatIconModule,MatChipsModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductDetailsComponent implements AfterViewInit {
  readonly dialog = inject(MatDialog);
  displayedColumns: string[] = ['workType', 'emissions', 'workDate', 'attachments'];
  dataSource = new MatTableDataSource<WorkElement>(workElements);
  userRole: string = "SCP";
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  addWork(){
    this.dialog.open(NewWorkDialogComponent)
  }

  modifyProduct(){
    this.dialog.open(EditProductDialogComponent)
  }

  isAdmin(): boolean{
    return this.userRole === "Admin";
  }
}

export interface WorkElement {
  workType: string;
  emissions: number;
  workDate: number;
  attachments: string;
}

const workElements: WorkElement[] = [
  { workType: "Construction", emissions: 120, workDate: 1709481600000, attachments: "report1.pdf" },
  { workType: "Transport", emissions: 85, workDate: 1709568000000, attachments: "logistics.pdf" },
  { workType: "Manufacturing", emissions: 200, workDate: 1709654400000, attachments: "factory_report.pdf" },
  { workType: "Agriculture", emissions: 150, workDate: 1709740800000, attachments: "soil_test.pdf" },
  { workType: "Energy Production", emissions: 300, workDate: 1709827200000, attachments: "power_data.pdf" },
  { workType: "Mining", emissions: 250, workDate: 1709913600000, attachments: "extraction_log.pdf" },
  { workType: "Recycling", emissions: 50, workDate: 1710000000000, attachments: "waste_management.pdf" },
  { workType: "Waste Disposal", emissions: 175, workDate: 1710086400000, attachments: "disposal_plan.pdf" },
  { workType: "Logistics", emissions: 110, workDate: 1710172800000, attachments: "shipment_details.pdf" },
  { workType: "Fishing", emissions: 90, workDate: 1710259200000, attachments: "catch_report.pdf" },
  { workType: "Forestry", emissions: 130, workDate: 1710345600000, attachments: "deforestation.pdf" },
  { workType: "Chemical Processing", emissions: 220, workDate: 1710432000000, attachments: "chemical_analysis.pdf" },
  { workType: "Retail", emissions: 60, workDate: 1710518400000, attachments: "sales_data.pdf" },
  { workType: "Construction", emissions: 140, workDate: 1710604800000, attachments: "building_permit.pdf" },
  { workType: "Agriculture", emissions: 170, workDate: 1710691200000, attachments: "crop_rotation.pdf" }
];
