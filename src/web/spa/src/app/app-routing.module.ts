import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogListComponent } from './catalog-list/catalog-list.component';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { InventoryListComponent } from './inventory-list/inventory-list.component';

const routes: Routes = [
    { path: '', component: DashboardComponent},
    { path: 'catalog', component: CatalogListComponent },
    { path: 'inventory', component: InventoryListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
