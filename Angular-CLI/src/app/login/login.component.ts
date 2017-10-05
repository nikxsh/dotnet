import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginRequest, LoginResponse } from '../Models/login.model';
import { MessageHandler } from '../Models/common.model';
import { LocalStorageService } from '../services/storage.service';
import { TenantService } from '../services/tenant.service';
import { AuthenticationService } from '../services/auth.service';
import * as Global from '../global'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  private loginRequest: LoginRequest = new LoginRequest();
  private loginResponse: LoginResponse = new LoginResponse();
  private loginLoader: boolean = false;
  private changePasswordMessage: string = '';
  private domainURL: string = '';
  private message: MessageHandler = new MessageHandler();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private storageService: LocalStorageService,
    private tenantService: TenantService
  ) { }

  ngOnInit() {
    try {
      this.authenticationService._logout();
      this.message.text = '';
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private login() {
    try {
      this.message.text = '';
      this.loginLoader = true;
      this.authenticationService._login(this.loginRequest)
        .then(data => {

          this.loginLoader = false;
          if (data.isAuthenticated && data.jwToken) {

            this.storageService._setAuthInfo(data);

            if (data.isFirstLogin)
              this.router.navigate(['app/changePassword']);
            else
              this.router.navigate(['app/main/index']);
          }
          else {
            this.message.text = data.message;
            this.message.type = 2;
          }
        },
        error => {
          this.loginLoader = false;
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
        });
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private resetLogin() {
    this.loginRequest.username = '';
    this.loginRequest.password = '';
    this.message.text = '';
  }

}