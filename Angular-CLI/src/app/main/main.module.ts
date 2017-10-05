import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Http, XHRBackend, RequestOptions, HttpModule } from '@angular/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; 

import { MainRoutingModule } from './main-routing.module';

import { ContactComponent } from './contact/contact.component';
import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';
import { TenantComponent } from './tenant/tenant.component';

import { TenantService } from '../services/tenant.service';
import { LocalStorageService } from '../services/storage.service';
import { UserService } from '../services/user.service';
import { RoleAndPermissionService } from '../services/rolepermissions.service';
import { CommonService } from '../services/common.service';

import { TitleCasePipe } from '../Pipes/titlecase.pipe';
import { HttpInterceptor } from '../interceptor';
import { IndexComponent } from './index/index.component';
import { RoleComponent } from './role/role.component';
import { ProductComponent } from './product/product.component';
import { ProductTableComponent } from './product/producttable.component';
import { ProductService } from '../services/product.service';
import { ProductCategoriesComponent } from './product/productcategories.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { InvoiceService } from '../services/invoice.service';
import { InvoiceTableComponent } from './invoice/invoicetable.component';
import { SalesOrderComponent } from './salesorder/salesorder.component';
import { SalesOrderService } from '../services/salesorder.service';
import { PurchaseOrderService } from '../services/purchaseorder.service';
import { BillService } from '../services/bill.service';
import { BillComponent } from './bill/bill.component';
import { BillTableComponent } from './bill/billtable.component';
import { PurchaseOrderComponent } from './purchaseorder/purchaseorder.component';
import { PurchaseOrderTableComponent } from './purchaseorder/purchaseordertable.component';
import { SalesOrderTableComponent } from './salesorder/salesordertable.component';
import { InventoryComponent } from './inventory/inventory.component';
import { ManageInventoryComponent } from './inventory/manageinventory.component';
import { InventoryWorkflowComponent } from './inventory/inventoryworkflow.component';
import { InventoryWipComponent } from './inventory/inventorywip.component';
import { InventoryService } from '../services/inventory.service';
import { PasswordChangeComponent } from './users/passwordchange.component';
import { AuthGuard } from '../auth.guard';

@NgModule({
  imports: [
    CommonModule, 
    FormsModule,
    HttpModule,
    ReactiveFormsModule, 
    MainRoutingModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    NgbModule.forRoot()
  ],
  declarations: [
    ContactComponent, 
    DashboardComponent, 
    UserComponent, 
    TenantComponent,
    TitleCasePipe,
    IndexComponent,
    RoleComponent,
    ProductComponent,
    ProductTableComponent,
    ProductCategoriesComponent,
    InvoiceComponent,
    InvoiceTableComponent,
    SalesOrderComponent,
    SalesOrderTableComponent,
    BillComponent,
    BillTableComponent,
    PurchaseOrderComponent,    
    PurchaseOrderTableComponent, 
    InventoryComponent, 
    ManageInventoryComponent, 
    InventoryWorkflowComponent, 
    InventoryWipComponent, 
    PasswordChangeComponent
  ],
  providers:[
    UserService,
    LocalStorageService,
    TenantService,
    RoleAndPermissionService,
    ProductService,
    InvoiceService,
    SalesOrderService,
    BillService,
    PurchaseOrderService,
    InventoryService,
    AuthGuard,
    CommonService,
    {
        provide: Http,
        useFactory: HttpInterceptorLoader,
        deps: [XHRBackend, RequestOptions, LocalStorageService]
    }
  ]
})

export class MainModule { }

export function HttpInterceptorLoader(xhrBackend :XHRBackend, requestOptions : RequestOptions, localServiceRef : LocalStorageService) {
    return new HttpInterceptor(xhrBackend, requestOptions, localServiceRef);
}
