import { Injectable } from '@angular/core';
import { Http, Headers, ConnectionBackend, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { LoginResponse } from './Models/login.model';
import { LocalStorageService } from './services/storage.service';


@Injectable()
export class HttpInterceptor extends Http {

    constructor(backend: ConnectionBackend, defaultOptions: RequestOptions, private storageServiceRef: LocalStorageService) {
        super(backend, defaultOptions);
    }

    private addTokenHeader(headers: Headers) {
        let SD323XX56 = this.storageServiceRef._getAuthInfo() as LoginResponse;
        if (SD323XX56)
            headers.append('Authorization', 'Bearer ' + SD323XX56.jwToken);
    }

    private addContentTypeJsonHeader(headers: Headers) {
        headers.append('Content-Type', 'application/json');
    }


    private addContentTypeMedia(headers: Headers) {
    }

    public get(url) {
        let headers = new Headers();
        this.addTokenHeader(headers);
        this.addContentTypeJsonHeader(headers);
        return super.get(url, {
            headers: headers
        });
    }

    public post(url, data) {
        let headers = new Headers();
        this.addTokenHeader(headers);

        if (data instanceof FormData)
            this.addContentTypeMedia(headers);
        else
            this.addContentTypeJsonHeader(headers);

        return super.post(url, data, {
            headers: headers
        });
    }
}