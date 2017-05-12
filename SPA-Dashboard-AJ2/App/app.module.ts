import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { RouterModule, Routes } from '@angular/router'
import { DashboardAppComponent } from './dashboard-app.component'
import { NavigationHeaderComponent } from './Navigation/header.component'
import { SideBarComponent } from './Navigation/sidebar.component'
import { MainBodyComponent } from './Navigation/main-body.component'
import { LinksComponent } from './Navigation/links.component'
import { UsersComponent } from './Users/users.component'
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

const appRoutes: Routes = [
    {
        path: '',
        component: MainBodyComponent
    },
    {
        path: 'index',
        component: MainBodyComponent
    },
    {
        path: 'users',
        component: UsersComponent
    }
];

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        DashboardAppComponent,
        NavigationHeaderComponent,
        SideBarComponent,
        MainBodyComponent,
        LinksComponent,
        UsersComponent
    ],
    providers: [
        {
            provide: LocationStrategy, useClass: HashLocationStrategy
        }
    ],
    bootstrap: [DashboardAppComponent]
})

export class AppModule {

}