import { Component } from '@angular/core'
import { ElementRef } from '@angular/core'

@Component({
    selector: 'users',
    templateUrl: './Templates/User/Users.html'
})

export class UsersComponent {

    //ElementRef: Provides access to the underlying native element (DOM element).
    constructor(private elementRef: ElementRef) { };

    ngAfterViewInit() {
        this.addCss("Content/css/dataTables.bootstrap.css");
        this.addCss("Content/css/dataTables.responsive.css");
    }

    addCss(link) {
        var s = document.createElement("link");
        s.rel = "stylesheet";
        s.href = link;
        this.elementRef.nativeElement.appendChild(s);
    };
} 