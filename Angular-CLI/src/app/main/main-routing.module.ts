import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { UserComponent } from './users/users.component';
import { TenantComponent } from './tenant/tenant.component';
import { IndexComponent } from './index/index.component';
import { ContactComponent } from './contact/contact.component';
import { RoleComponent } from './role/role.component';
import { ProductComponent } from './product/product.component';
import { ProductcategoriesComponent } from './product/productcategories.component';

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
      },
      {
        path:'contacts/:type',
        component:ContactComponent 
      },
      {
        path:'roles',
        component:RoleComponent 
      },
      {
        path:'products',
        component:ProductComponent 
      },
      {
        path:'pcategories',
        component:ProductcategoriesComponent 
      }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
