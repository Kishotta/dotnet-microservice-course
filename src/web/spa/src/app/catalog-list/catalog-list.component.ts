import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CatalogService } from '../services/catalog.service';
import { CatalogAddFormComponent } from '../catalog-add-form/catalog-add-form.component';
import { CatalogEditFormComponent } from '../catalog-edit-form/catalog-edit-form.component';
import { InventoryGrantFormComponent } from '../inventory-grant-form/inventory-grant-form.component';

interface CatalogItem {
    id: string;
    name: string;
    description: string;
    price: number;
    createdDate: Date;
}

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.scss']
})
export class CatalogListComponent implements OnInit {
    displayedColumns: string[] = ['name', 'description', 'price', 'createdDate', 'actions'];
    dataSource!: MatTableDataSource<CatalogItem>;

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(
        private _dialog: MatDialog,
        private _catalogService: CatalogService
    ) {}

    ngOnInit() {
        this.getCatalogItemsList();
    }

    getCatalogItemsList() {
        this._catalogService
            .getCatalogItems()
            .subscribe({
                next: (data: any) => {
                    this.dataSource = new MatTableDataSource(data);
                    this.dataSource.sort = this.sort;
                    this.dataSource.paginator = this.paginator;
                },
                error: console.log,
            });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
          this.dataSource.paginator.firstPage();
        }
    }

    openAddCatalogItemForm() {
        const dialogRef = this._dialog.open(CatalogAddFormComponent);
        dialogRef.afterClosed()
            .subscribe({
                next: (success) => {
                    if (success) {
                        this.getCatalogItemsList()
                    }
                },
                error: console.log,
            });
    }

    openEditCatalogItemForm(data: any) {
        const dialogRef = this._dialog.open(CatalogEditFormComponent, { data });
        dialogRef.afterClosed()
            .subscribe({
                next: (success) => {
                    if (success) {
                        this.getCatalogItemsList()
                    }
                },
                error: console.log,
            });
    }

    openGrantInventoryItemForm(data: any) {
        const dialogRef = this._dialog.open(InventoryGrantFormComponent, { data });
        dialogRef.afterClosed()
            .subscribe({
                next: (success) => {
                    if (success) {
                        this.getCatalogItemsList()
                    }
                },
                error: console.log,
            });
    }

    deleteCatalogItem(id: string) {
        this._catalogService
            .deleteCatalogItem(id)
            .subscribe({
                next: () => this.getCatalogItemsList(),
                error: console.log,
            });
    }
}
