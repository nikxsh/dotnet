import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { MainRoutingModule } from './main-routing.module';
import { ContactComponent } from './contact/contact.component';
import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TenantComponent } from './tenant/tenant.component';
import { TitleCasePipe } from '../Pipes/titlecase.p';
import { TenantService } from '../services/tenant.s';
import { LocalStorageService } from '../services/storage.s';
import { UserService } from '../services/user.s';
import { Http, XHRBackend, RequestOptions, HttpModule } from '@angular/http';
import { HttpInterceptor } from '../interceptor.h';
import { RoleAndPermissionService } from '../services/rolepermissions.s';

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
    TitleCasePipe
  ],
  providers:[
    UserService,
    LocalStorageService,
    TenantService,
    RoleAndPermissionService,
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
