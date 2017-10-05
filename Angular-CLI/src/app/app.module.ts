import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { RouteRoutingModule } from './route-routing.module';
import { PageNotFoundComponent } from './page-not-found.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, XHRBackend, RequestOptions, Http } from '@angular/http';
import { AuthenticationService } from './services/auth.service';
import { LocalStorageService } from './services/storage.service';
import { TenantService } from './services/tenant.service';
import { HttpInterceptor } from './interceptor';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,   
    FormsModule,
    ReactiveFormsModule,
    HttpModule, 
    RouteRoutingModule    
  ],
  providers: [
    AuthenticationService,
    LocalStorageService,
    TenantService,
    LocalStorageService,
    {
        provide: Http,
        useFactory: HttpInterceptorLoader,
        deps: [XHRBackend, RequestOptions, LocalStorageService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpInterceptorLoader(xhrBackend :XHRBackend, requestOptions : RequestOptions, localServiceRef : LocalStorageService) {
  return new HttpInterceptor(xhrBackend, requestOptions, localServiceRef);
}