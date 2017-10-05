import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { AuthenticationService } from '../services/auth.service';
import { PasswordResetComponent } from './password-reset.component';
import { LogoutComponent } from './logout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { LocalStorageService } from '../services/storage.service';
import { TenantService } from '../services/tenant.service';

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
    LogoutComponent
  ],
  providers: [
    AuthenticationService,
    TenantService,
    LocalStorageService
  ]
})
export class LoginModule { }
