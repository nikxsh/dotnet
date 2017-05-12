"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var LinksComponent = (function () {
    function LinksComponent() {
        this.GetMessages = [
            {
                id: 1, name: 'Nikhilesh Shinde', date: 'Today', message: 'Hello there!!!'
            },
            {
                id: 1, name: 'Asawari lol', date: 'Yesterday', message: 'Why this kolawari'
            },
            {
                id: 1, name: 'John Smith', date: 'Yesterday', message: 'Sky full of lighters!'
            }
        ];
    }
    return LinksComponent;
}());
LinksComponent = __decorate([
    core_1.Component({
        selector: 'top-links',
        templateUrl: './Templates/Navigation/Links.html'
    })
], LinksComponent);
exports.LinksComponent = LinksComponent;
//# sourceMappingURL=links.component.js.map