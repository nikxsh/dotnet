import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { LoginRequest } from '../../Models/login.model';
import { AuthenticationService } from '../../services/auth.service';
import { LocalStorageService } from '../../services/storage.service';
import { matchingPasswords } from '../../helpers/validators.utility';


@Component({
  selector: 'app-passwordchange',
  templateUrl: './passwordchange.component.html',
  styleUrls: ['./passwordchange.component.css']
})
export class PasswordChangeComponent implements OnInit {
    private changePasswordLoader: boolean;
    private changePasswordForm: FormGroup;
    private loginRequest: LoginRequest = new LoginRequest();
    private changePasswordMessage: string = '';

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

        this.changePasswordForm = this.formBuilder.group({
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required]
        }, { validator: matchingPasswords('password', 'confirmPassword') })
    }

    private changePassword() {
        try {
            this.changePasswordLoader = true;
            let authInfo = this.storageService._getAuthInfo();
            this.authenticationService._changePassword(authInfo.userId, this.loginRequest.password)
                .then(data => {

                    this.changePasswordLoader = false;
                    if (data) {
                        this.storageService._removeAuthInfo();
                        this.router.navigate(['app/login']);
                    }
                    else
                        this.changePasswordMessage = 'Unable to process the request';
                },
                error => {
                    this.changePasswordLoader = false;
                    this.changePasswordMessage = 'Unable to process the request';
                });
        }
        catch (exeception) {        
        }
    }
}