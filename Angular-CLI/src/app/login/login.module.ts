import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { AuthenticationService } from '../services/auth.service';
import { PasswordResetComponent } from './password-reset.component';
import { LogoutComponent } from './logout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, Http, XHRBackend, RequestOptions } from '@angular/http';
import { LocalStorageService } from '../services/storage.service';
import { TenantService } from '../services/tenant.service';
import { HttpInterceptor } from '../interceptor';
import { PasswordChangeComponent } from './passwordchange.component';
import { AuthGuard } from '../auth.guard';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    LoginRoutingModule
  ],
  declarations: [
    LoginComponent,
    PasswordResetComponent,
    PasswordChangeComponent,
    LogoutComponent
  ],
  providers: [
    AuthenticationService,
    TenantService,
    LocalStorageService,
    AuthGuard,
    {
      provide: Http,
      useFactory: HttpInterceptorLoader,
      deps: [XHRBackend, RequestOptions, LocalStorageService]
  }
  ]
})
export class LoginModule { }

export function HttpInterceptorLoader(xhrBackend :XHRBackend, requestOptions : RequestOptions, localServiceRef : LocalStorageService) {
  return new HttpInterceptor(xhrBackend, requestOptions, localServiceRef);
}