import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';

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
        path:'users',
        component:UserComponent 
      }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
