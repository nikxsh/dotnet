import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';
import { TenantComponent } from './tenant/tenant.component';
import { IndexComponent } from './index/index.component';
import { ContactComponent } from './contact/contact.component';
import { RoleComponent } from './role/role.component';
import { ProductComponent } from './product/product.component';
import { ProductCategoriesComponent } from './product/productcategories.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { InvoiceTableComponent } from './invoice/invoicetable.component';
import { SalesOrderComponent } from './salesorder/salesorder.component';
import { BillComponent } from './bill/bill.component';
import { BillTableComponent } from './bill/billtable.component';
import { PurchaseOrderTableComponent } from './purchaseorder/purchaseordertable.component';
import { PurchaseOrderComponent } from './purchaseorder/purchaseorder.component';
import { SalesOrderTableComponent } from './salesorder/salesordertable.component';
import { InventoryComponent } from './inventory/inventory.component';
import { InventoryWipComponent } from './inventory/inventorywip.component';
import { InventoryWorkflowComponent } from './inventory/inventoryworkflow.component';
import { ManageInventoryComponent } from './inventory/manageinventory.component';
import { AuthGuard } from '../auth.guard';
import { PasswordChangeComponent } from './users/passwordchange.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'main',
    pathMatch: 'full'
  },
  {
    path: 'main',
    component: DashboardComponent,
    children: [
      {
          path: 'changePassword',
          component: PasswordChangeComponent,
          canActivate: [AuthGuard]
      },
      {
        path: 'dashboard',
        component: IndexComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'users',
        component: UserComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'oprofile',
        component: TenantComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'contacts/:type',
        component: ContactComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'roles',
        component: RoleComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'products',
        component: ProductComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'pcategories',
        component: ProductCategoriesComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'addinvoice',
        component: InvoiceComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'addinvoice/sale/:saleId',
        component: InvoiceComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'editinvoice/:id',
        component: InvoiceComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'editinvoice/:id/:saleId',
        component: InvoiceComponent,
        canActivate: [AuthGuard]
      },
      {
          path: 'invoices',
          component: InvoiceTableComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'addsalesorder',
          component: SalesOrderComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'editsalesorder/:id',
          component: SalesOrderComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'salesorders',
          component: SalesOrderTableComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'addbill',
          component: BillComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'addbill/:purchaseId/:recieveNumber',
          component: BillComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'editbill/:id',
          component: BillComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'editbill/:id/:purchaseId/:recieveNumber',
          component: BillComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'bills',
          component: BillTableComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'addpurchaseorders',
          component: PurchaseOrderComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'editpurchaseorder/:id',
          component: PurchaseOrderComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'purchaseorders',
          component: PurchaseOrderTableComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'invdashboard',
          component: InventoryComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'wipinvdashboard',
          component: InventoryWipComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'invworkflow',
          component: InventoryWorkflowComponent,
          canActivate: [AuthGuard]
      },
      {
          path: 'invmanage',
          component: ManageInventoryComponent,
          canActivate: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
