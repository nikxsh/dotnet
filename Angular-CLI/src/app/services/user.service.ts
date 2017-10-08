import { Injectable } from '@angular/core'
import { Headers, Response, RequestOptions, Http } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';
                                                                 
import * as Global from '../global';
import { ResponseBase } from '../helpers/adapter.utility';
import { UserResponse, User } from '../Models/user.model';
import { PagingRequest } from '../Models/common.model';
import { HandleError } from '../helpers/error.utility';

@Injectable()
export class UserService {
    
    constructor(private httpRef: Http) {
    }

    public _getAllUsers(take, skip): Promise<ResponseBase<UserResponse[]>> {

        let body = JSON.stringify(new PagingRequest(take, skip));

        return this.httpRef.post(Global.API_GET_ALL_USER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<UserResponse[]>)
            .catch(HandleError.handle);
    }

    public _addUser(request: User): Promise<ResponseBase<UserResponse>> {

        let body = JSON.stringify(request);

        return this.httpRef.post(Global.API_ADD_USER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<UserResponse>)
            .catch(HandleError.handle);
    }

    public _editUser(request: User): Promise<ResponseBase<User>> {

        let body = JSON.stringify(request);

        return this.httpRef.post(Global.API_EDIT_USER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<User>)
            .catch(HandleError.handle);
    }

    public _enableDisableUser(id, isEnabled): Promise<ResponseBase<any>> {

        let body = JSON.stringify({ id : id, IsEnable : isEnabled });

        return this.httpRef.post(Global.API_ENABLE_DISABLE_USER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }
}


