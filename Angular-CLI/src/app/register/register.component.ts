import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RegisterRequest } from '../Models/login.model';
import { MessageHandler } from '../Models/common.model';
import { AuthenticationService } from '../services/auth.service';
import { LocalStorageService } from '../services/storage.service';
import { TenantService } from '../services/tenant.service';
import * as Global from '../global'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
                                                               
    private registerRequest: RegisterRequest = new RegisterRequest();   
    private registerLoader: boolean = false;     
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
        }
        catch (exeception) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }          

    private register() {
        try {
            this.message.text = '';
            this.registerLoader = true;

            this.tenantService._register(this.registerRequest)
                .then(result => {

                    this.registerLoader = false;

                    if (result.status == 1) {
                        this.resetRegistration();
                        this.message.type = 1;
                        this.message.text = "Registration is successfull. Please check your e-mail!";
                    }
                    else {
                        this.message.type = 2;
                        this.message.text = result.message;
                    }
                },
                error => {
                    this.registerLoader = false;
                    this.message.text = Global.UI_ERROR;
                    this.message.type = 2;
                });
        }
        catch (exeception) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private configureDomainName(domain: string) {
        try {
            if (domain != '' && domain.length > 0)
                this.domainURL = 'www.' + this.registerRequest.domainName + '.brewbuck.com';
            else
                this.domainURL = '';
        }
        catch (exeception) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }             

    private resetRegistration() {
        this.registerRequest.name = '';
        this.registerRequest.domainName = '';
        this.registerRequest.email = '';
        this.registerRequest.phone = '';
        this.message.text = '';
        this.registerRequest.termsAgreed = false;
        this.domainURL = '';
    }
}