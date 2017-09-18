import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from '../app.component';
import { UserComponent } from '../components/user/user.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    redirectTo:'app',
    pathMatch: 'full'
  },
  {
    path: 'app',
    component:DashboardComponent,
    children : [
      {
        path:'users',
        component: UserComponent
      }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RouteRoutingModule { }
