import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { ContactComponent } from './contact/contact.component';
import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';

@NgModule({
  imports: [
    CommonModule,
    MainRoutingModule
  ],
  declarations: [ContactComponent, DashboardComponent, UserComponent]
})
export class MainModule { }
