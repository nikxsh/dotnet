"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var dashboard_app_component_1 = require("./dashboard-app.component");
var header_component_1 = require("./Navigation/header.component");
var sidebar_component_1 = require("./Navigation/sidebar.component");
var main_body_component_1 = require("./Navigation/main-body.component");
var links_component_1 = require("./Navigation/links.component");
var users_component_1 = require("./Users/users.component");
var common_1 = require("@angular/common");
var appRoutes = [
    {
        path: '',
        component: main_body_component_1.MainBodyComponent
    },
    {
        path: 'index',
        component: main_body_component_1.MainBodyComponent
    },
    {
        path: 'users',
        component: users_component_1.UsersComponent
    }
];
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            router_1.RouterModule.forRoot(appRoutes)
        ],
        declarations: [
            dashboard_app_component_1.DashboardAppComponent,
            header_component_1.NavigationHeaderComponent,
            sidebar_component_1.SideBarComponent,
            main_body_component_1.MainBodyComponent,
            links_component_1.LinksComponent,
            users_component_1.UsersComponent
        ],
        providers: [
            {
                provide: common_1.LocationStrategy, useClass: common_1.HashLocationStrategy
            }
        ],
        bootstrap: [dashboard_app_component_1.DashboardAppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map