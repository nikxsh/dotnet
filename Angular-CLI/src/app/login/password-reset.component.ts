import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { ResetPassword } from '../Models/login.model';
import { AuthenticationService } from '../services/auth.service';
import { LocalStorageService } from '../services/storage.service';
import { AtLeastOneFieldValidator } from '../helpers/validators.utility';


@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {
    private resetPasswordLoader: boolean;
    private resetPasswordForm: FormGroup;
    private resetPasswordRequest: ResetPassword = new ResetPassword();
    private resetPasswordMessage: string = '';


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private authenticationService: AuthenticationService,
        private storageService: LocalStorageService
    ) {
    }

    ngOnInit(): void {
        this.InItUserForm();
    }

    private InItUserForm() {

        this.resetPasswordForm = this.formBuilder.group({
            name: ['', Validators.nullValidator]
        }, { validator: AtLeastOneFieldValidator })
    }

    private resetPassword() {
        try {
            this.resetPasswordLoader = true;
            this.authenticationService._resetPassword(this.resetPasswordRequest.name)
                .then(data => {

                    this.resetPasswordLoader = false;
                    if (data) {
                        this.storageService._removeAuthInfo();
                        this.router.navigate(['login/init']);
                    }
                    else
                        this.resetPasswordMessage = 'Unable to process the request';
                },
                error => {
                    this.resetPasswordLoader = false;
                    this.resetPasswordMessage = 'Unable to process the request';
                });
        }
        catch (exeception) {        
        }
    }
}