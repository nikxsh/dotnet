import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/auth.service';
import { LocalStorageService } from '../services/storage.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent  {
  
      constructor(
          private router: Router,
          private authenticationService: AuthenticationService,
          private storageService: LocalStorageService
      ) { }
  
      logOut() {
          // reset login status
          //this.authenticationService._logout();
          this.storageService._removeAuthInfo();
          this.router.navigate(['app/login']);
      }
  
  }
