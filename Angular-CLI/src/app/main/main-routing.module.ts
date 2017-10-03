import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';
import { TenantComponent } from './tenant/tenant.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  {
    path: '',
    redirectTo:'main',
    pathMatch: 'full'
  },
  {
    path:'main',
    component:DashboardComponent,
    children:[ 
      {
        path:'dashboard',
        component: IndexComponent 
      },
      {
        path:'users',
        component:UserComponent 
      },
      {
        path:'oprofile',
        component:TenantComponent 
      }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
