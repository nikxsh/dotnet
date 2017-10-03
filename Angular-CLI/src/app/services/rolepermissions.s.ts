import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';

import * as Global from '../global'
import { ModulePermission, Role } from "../Models/user.m";
import { ResponseBase } from '../helpers/adapter.h';
import { HandleError } from '../helpers/error.h';

@Injectable()
export class RoleAndPermissionService {
    
    constructor(private httpRef: Http) {
    }

    public _getTenantRoles(IsEnabledRoles): Promise<ResponseBase<Role[]>> {

        let body = JSON.stringify({ IsEnable: IsEnabledRoles });
        return this.httpRef.post(Global.API_GET_TENANT_ROLES, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Role[]>)
            .catch(HandleError.handle);
    }   
    public _getPermissionForRole(roleId): Promise<ResponseBase<Role>> {

        let body = JSON.stringify({ id: roleId });
        return this.httpRef.post(Global.API_GET_PERMISSION_FOR_ROLE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Role>)
            .catch(HandleError.handle);
    }    

    public _savePermissionForRole(role: Role, updatedPermissions: ModulePermission[]): Promise<ResponseBase<ModulePermission[]>> {

        role.modulePermissions = updatedPermissions;

        let body = JSON.stringify(role);

        return this.httpRef.post(Global.API_SAVE_PERMISSION_FOR_ROLE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<ModulePermission[]>)
            .catch(HandleError.handle);
    }

    public _saveRole(role): Promise<ResponseBase<Role>> {

        let body = JSON.stringify(role);

        return this.httpRef.post(Global.API_SAVE_ROLE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Role>)
            .catch(HandleError.handle);
    }

    public _editRole(role): Promise<ResponseBase<Role>> {

        let body = JSON.stringify(role);

        return this.httpRef.post(Global.API_EDIT_ROLE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Role>)
            .catch(HandleError.handle);
    }

    public _enableDisableRole(id, isEnabled): Promise<ResponseBase<Role>> {

        let body = JSON.stringify({ id: id, IsEnable: isEnabled });

        return this.httpRef.post(Global.API_ENABLE_DISABLE_ROLE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Role>)
            .catch(HandleError.handle);
    }
}


