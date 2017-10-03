import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Http, XHRBackend, RequestOptions, HttpModule } from '@angular/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

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
  ],
  declarations: [
    ContactComponent, 
    DashboardComponent, 
    UserComponent, 
    TenantComponent,
    TitleCasePipe,
    IndexComponent
  ],
  providers:[
    UserService,
    LocalStorageService,
    TenantService,
    RoleAndPermissionService,
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
