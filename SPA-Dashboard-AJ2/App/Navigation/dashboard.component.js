"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var DashboardComponent = (function () {
    function DashboardComponent(elementRef) {
        this.elementRef = elementRef;
    }
    ;
    DashboardComponent.prototype.ngAfterViewInit = function () {
        this.addScript("Content/scripts/dashboard/morris-data.js");
    };
    ;
    DashboardComponent.prototype.addScript = function (link) {
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = "\n$(function () {\n\n    Morris.Area({\n        element: 'morris-area-chart',\n        data: [{\n            period: '2010 Q1',\n            iphone: 2666,\n            ipad: null,\n            itouch: 2647\n        }, {\n            period: '2010 Q2',\n            iphone: 2778,\n            ipad: 2294,\n            itouch: 2441\n        }, {\n            period: '2010 Q3',\n            iphone: 4912,\n            ipad: 1969,\n            itouch: 2501\n        }, {\n            period: '2010 Q4',\n            iphone: 3767,\n            ipad: 3597,\n            itouch: 5689\n        }, {\n            period: '2011 Q1',\n            iphone: 6810,\n            ipad: 1914,\n            itouch: 2293\n        }, {\n            period: '2011 Q2',\n            iphone: 5670,\n            ipad: 4293,\n            itouch: 1881\n        }, {\n            period: '2011 Q3',\n            iphone: 4820,\n            ipad: 3795,\n            itouch: 1588\n        }, {\n            period: '2011 Q4',\n            iphone: 15073,\n            ipad: 5967,\n            itouch: 5175\n        }, {\n            period: '2012 Q1',\n            iphone: 10687,\n            ipad: 4460,\n            itouch: 2028\n        }, {\n            period: '2012 Q2',\n            iphone: 8432,\n            ipad: 5713,\n            itouch: 1791\n        }],\n        xkey: 'period',\n        ykeys: ['iphone', 'ipad', 'itouch'],\n        labels: ['iPhone', 'iPad', 'iPod Touch'],\n        pointSize: 2,\n        hideHover: 'auto',\n        resize: true\n    });\n\n    Morris.Donut({\n        element: 'morris-donut-chart',\n        data: [{\n            label: \"Download Sales\",\n            value: 12\n        }, {\n            label: \"In-Store Sales\",\n            value: 30\n        }, {\n            label: \"Mail-Order Sales\",\n            value: 20\n        }],\n        resize: true\n    });\n\n    Morris.Bar({\n        element: 'morris-bar-chart',\n        data: [{\n            y: '2006',\n            a: 100,\n            b: 90\n        }, {\n            y: '2007',\n            a: 75,\n            b: 65\n        }, {\n            y: '2008',\n            a: 50,\n            b: 40\n        }, {\n            y: '2009',\n            a: 75,\n            b: 65\n        }, {\n            y: '2010',\n            a: 50,\n            b: 40\n        }, {\n            y: '2011',\n            a: 75,\n            b: 65\n        }, {\n            y: '2012',\n            a: 100,\n            b: 90\n        }],\n        xkey: 'y',\n        ykeys: ['a', 'b'],\n        labels: ['Series A', 'Series B'],\n        hideHover: 'auto',\n        resize: true\n    });\n\n    var year_data = [\n  { \"period\": \"2012\", \"licensed\": 3407, \"sorned\": 660 },\n  { \"period\": \"2011\", \"licensed\": 3351, \"sorned\": 629 },\n  { \"period\": \"2010\", \"licensed\": 3269, \"sorned\": 618 },\n  { \"period\": \"2009\", \"licensed\": 3246, \"sorned\": 661 },\n  { \"period\": \"2008\", \"licensed\": 3257, \"sorned\": 667 },\n  { \"period\": \"2007\", \"licensed\": 3248, \"sorned\": 627 },\n  { \"period\": \"2006\", \"licensed\": 3171, \"sorned\": 660 },\n  { \"period\": \"2005\", \"licensed\": 3171, \"sorned\": 676 },\n  { \"period\": \"2004\", \"licensed\": 3201, \"sorned\": 656 },\n  { \"period\": \"2003\", \"licensed\": 3215, \"sorned\": 622 }\n    ];\n    Morris.Line({\n        element: 'morris-line-chart',\n        data: year_data,\n        xkey: 'period',\n        ykeys: ['licensed', 'sorned'],\n        labels: ['Licensed', 'SORN']\n    });\n\n});\n\n";
        this.elementRef.nativeElement.appendChild(s);
    };
    ;
    return DashboardComponent;
}());
DashboardComponent = __decorate([
    core_1.Component({
        selector: 'dashboard',
        templateUrl: './Templates/Dashboard/Dashboard.html'
    }),
    __metadata("design:paramtypes", [core_2.ElementRef])
], DashboardComponent);
exports.DashboardComponent = DashboardComponent;
//# sourceMappingURL=dashboard.component.js.map