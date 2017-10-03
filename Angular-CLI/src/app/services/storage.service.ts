import { Injectable } from '@angular/core';
import { Tenant } from '../Models/profile.model';

@Injectable()
export class LocalStorageService {

    private authItem: string = 'SDF2334S-123SASFS-DF123DSD-SXXX';  
    private tenantItem: string = '123123S-AXASXS-A123000-0POQWED';
    private gstItem: string = '123123S-42343423-A123000-0POQWED';
    constructor() {
    }

    public _setAuthInfo(value: any) {
        localStorage.setItem(this.authItem, JSON.stringify(value)); 
    }

    public _getAuthInfo() {
        let authInfo = localStorage.getItem(this.authItem);
        return JSON.parse(authInfo);
    }

    public _removeAuthInfo() {
        localStorage.removeItem(this.authItem);
    }

    public _setTenantInfo(tenantInfo: Tenant) {
        localStorage.setItem(this.tenantItem, JSON.stringify(tenantInfo));
    }

    public _getTenantInfo() {                        
        return localStorage.getItem(this.tenantItem);
    }        
}