import { Component, OnInit, ElementRef } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentDateTime: Date;

  constructor(private elementRef: ElementRef) { 
    this.utcTime();
  };

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.addScript("assets/scripts/site.js");
    this.addCss("assets/css/metis-menu.css");
  }

  addScript(link) {
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = link;
    this.elementRef.nativeElement.appendChild(s);
  }

  addCss(link) {
    var s = document.createElement("link");
    s.rel = "stylesheet";
    s.href = link;
    this.elementRef.nativeElement.appendChild(s);
  }

  private utcTime() {
    setInterval(() => {
        this.currentDateTime = new Date();
    }, 1000);
}
}
