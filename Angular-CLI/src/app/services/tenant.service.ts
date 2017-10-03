import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';


import { RegisterRequest } from '../Models/login.model';
import { Tenant } from '../Models/profile.model';
import { PagingRequest } from '../Models/common.model';
import { Catalogue } from '../Models/contact.model';

import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import * as Global from '../global'

@Injectable()
export class TenantService {
    
    constructor(private httpRef: Http) {
    }

    public _register(request: RegisterRequest): Promise<ResponseBase<Tenant>> {

        let body = JSON.stringify(request);

        return this.httpRef.post(Global.API_REGISTER_TENANT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Tenant>)
            .catch(HandleError.handle);
    }

    public _getTenantProfile(): Promise<ResponseBase<Tenant>> {

        return this.httpRef.get(Global.API_GET_TENANT_PROFILE)
            .toPromise()
            .then(response => response.json() as ResponseBase<Tenant>)
            .catch(HandleError.handle);
    }

    public _saveTenantProfile(tenantInfo: Tenant): Promise<ResponseBase<Tenant>> {

        let body = JSON.stringify(tenantInfo);

        return this.httpRef.post(Global.API_SAVE_TENANT_PROFILE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Tenant>)
            .catch(HandleError.handle);
    }

    public _uploadLogo(image: File): Promise<ResponseBase<string>> {

        let formData: FormData = new FormData();
        formData.append('uploadFile', image, image.name); 

        return this.httpRef.post(Global.API_UPLOAD_TENANT_LOGO, formData)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _getCustomerContacts(request: PagingRequest): Promise<ResponseBase<Catalogue[]>> {

        let body = JSON.stringify(request);                                                                      

        return this.httpRef.post(Global.API_GET_TENANT_CUSTOMERS_CONTACTS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue[]>)
            .catch(HandleError.handle);
    }

    public _getVendorContacts(request: PagingRequest): Promise<ResponseBase<Catalogue[]>> {

        let body = JSON.stringify(request);
                                                                                                                 
        return this.httpRef.post(Global.API_GET_TENANT_VENDORS_CONTACTS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue[]>)
            .catch(HandleError.handle);
    }

    public _addCustomerContact(catalogueAddRequest, type: number): Promise<ResponseBase<Catalogue>> {

        let body = JSON.stringify(catalogueAddRequest);          
                                                       
        return this.httpRef.post(Global.API_ADD_TENANT_CUSTOMER_CONTACT , body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue>)
            .catch(HandleError.handle);
    }

    public _addVendorContact(catalogueAddRequest, type: number): Promise<ResponseBase<Catalogue>> {

        let body = JSON.stringify(catalogueAddRequest);   

        return this.httpRef.post(Global.API_ADD_TENANT_VENDOR_CONTACT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue>)
            .catch(HandleError.handle);
    }

    public _editCustomerContact(catalogueAddRequest: Catalogue, type: number): Promise<ResponseBase<Catalogue>> {

        let body = JSON.stringify(catalogueAddRequest);    

        return this.httpRef.post(Global.API_EDIT_TENANT_CUSTOMER_CONTACT , body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue>)
            .catch(HandleError.handle);
    }

    public _editVendorContact(catalogueAddRequest: Catalogue, type: number): Promise<ResponseBase<Catalogue>> {

        let body = JSON.stringify(catalogueAddRequest);                        

        return this.httpRef.post(Global.API_EDIT_TENANT_VENDOR_CONTACT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue>)
            .catch(HandleError.handle);
    }

    public _enableDisableContact(id, isEnabled, type: number): Promise<ResponseBase<Catalogue>> {

        let body = JSON.stringify({ id: id, IsEnable: isEnabled });

        let path = type == 0 ? Global.API_ENABLE_DISABLE_CUSTOMER_CONTACT : Global.API_ENABLE_DISABLE_VENDOR_CONTACT;

        return this.httpRef.post(path, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Catalogue>)
            .catch(HandleError.handle);
    }

    public _checkFileExistance(path): Promise<any> {               

        return this.httpRef.head(path)
            .toPromise()
            .then(response => true)
            .catch(HandleError.handle);
    }

}


