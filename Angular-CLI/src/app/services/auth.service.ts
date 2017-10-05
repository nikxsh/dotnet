import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';
import { LocalStorageService } from './storage.service';
import { LoginRequest, LoginResponse } from '../Models/login.model';

import * as Global from '../global'
import { HandleError } from '../helpers/error.utility';

@Injectable()
export class AuthenticationService {

    constructor(private http: Http,
        private storageService: LocalStorageService) {
    }

    public _login(request: LoginRequest): Promise<LoginResponse> {

        let body = JSON.stringify({ name: request.username, password: request.password });

        //The Angular http.post returns an RxJS Observable.
        //Hence, Convert the Observable to a Promise using the toPromise operator.
        return this.http.post(Global.API_LOGIN, body)
            .toPromise()
            .then(response => response.json() as LoginResponse)
            .catch(HandleError.handle);
    }

    public _changePassword(userId, password): Promise<string> {

        let body = JSON.stringify({ id: userId,  password: password });

        //The Angular http.post returns an RxJS Observable.
        //Hence, Convert the Observable to a Promise using the toPromise operator.
        return this.http.post(Global.API_CHANGE_PASSWORD, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _resetPassword(username): Promise<string> {

        let body = JSON.stringify({ name: username});

        //The Angular http.post returns an RxJS Observable.
        //Hence, Convert the Observable to a Promise using the toPromise operator.
        return this.http.post(Global.API_RESET_PASSWORD, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _logout() {
        this.storageService._removeAuthInfo();
    }
}


