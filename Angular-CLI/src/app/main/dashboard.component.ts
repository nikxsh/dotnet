import { Component, OnInit, ElementRef } from '@angular/core';
import { TenantService } from '../services/tenant.service';
import { Tenant, Profile } from '../Models/profile.model';
import { HandleError } from '../helpers/error.utility';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  tenantInfo: Tenant = new Tenant();
  currentDateTime: Date;

  constructor(private elementRef: ElementRef,
    private tenantService: TenantService) {
    this.utcTime();
    this.tenantInfo.orgProfile = new Profile();
  };

  ngOnInit() {
    this.getTenantInfo();
  }

  ngAfterViewInit() {
    this.addScript("assets/scripts/site.js");
    //this.addCss("assets/css/metis-menu.css");
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

  private getTenantInfo() {
    try {
      this.tenantService._getTenantProfile()
        .then(result => {
          if (result.status == 1)
            this.tenantInfo = result.data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }


  private utcTime() {
    setInterval(() => {
      this.currentDateTime = new Date();
    }, 1000);
  }
}
