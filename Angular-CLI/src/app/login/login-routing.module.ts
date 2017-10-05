import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { PasswordResetComponent } from './password-reset.component';
import { PasswordChangeComponent } from './passwordchange.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'init',
    pathMatch: 'full'
  },
  {
    path: 'init',
    component: LoginComponent
  },
  {
    path: 'forgotPassword',
    component: PasswordResetComponent
  },
  {
    path: 'changePassword',
    component: PasswordChangeComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
