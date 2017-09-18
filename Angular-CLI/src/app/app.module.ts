import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RouteRoutingModule } from './route/route-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,    
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    RouteRoutingModule    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
