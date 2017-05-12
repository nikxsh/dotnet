import { Component } from '@angular/core'
import { ElementRef } from '@angular/core'

@Component({
    selector: 'sidebar',
    templateUrl: './Templates/Navigation/Sidebar.html'
})

export class SideBarComponent {

    //ElementRef: Provides access to the underlying native element (DOM element).
    constructor(private elementRef: ElementRef) { };

    ngAfterViewInit() {
        this.addScript("Content/scripts/dashboard/dashboard.js");
        this.addScript("Content/scripts/dashboard/metisMenu.js");
        this.addCss("Content/css/metisMenu.css");
    }

    addScript(link) {
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.src = link;
        this.elementRef.nativeElement.appendChild(s);
    };

    addCss(link) {
        var s = document.createElement("link");
        s.rel = "stylesheet";
        s.href = link;
        this.elementRef.nativeElement.appendChild(s);
    };
} 