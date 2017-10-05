import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';                           

import * as Global from '../global'
import { InventoryPagingRequest } from '../Models/common.model';
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import { InventoryWorkflow } from '../models/inventory.model';

@Injectable()
export class InventoryService {

    constructor(private http: Http) {
    }

    public _getAllProductInventory(request: InventoryPagingRequest): Promise<ResponseBase<any[]>> {
        let body = JSON.stringify(request); 
        return this.http.post(Global.API_GET_ALL_INVENTORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any[]>)
            .catch(HandleError.handle);
    }

    public _getAllWipProductInventory(): Promise<ResponseBase<any[]>> {
        return this.http.get(Global.API_GET_ALL_WIP_INVENTORY)
            .toPromise()
            .then(response => response.json() as ResponseBase<any[]>)
            .catch(HandleError.handle);
    }

    public _getProductInventoryHistory(request: InventoryPagingRequest): Promise<ResponseBase<any[]>> {
        let body = JSON.stringify(request);
        return this.http.post(Global.API_GET_INVENTORY_HISTORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any[]>)
            .catch(HandleError.handle);
    } 

    public _getAllInventoryWorkflows(): Promise<ResponseBase<InventoryWorkflow[]>> {
        return this.http.get(Global.API_GET_ALL_INVENTORY_WORKFLOWS)
            .toPromise()
            .then(response => response.json() as ResponseBase<InventoryWorkflow[]>)
            .catch(HandleError.handle);
    }

    public _getAllInventoryWorkflowsWIP(): Promise<ResponseBase<InventoryWorkflow[]>> {
        return this.http.get(Global.API_GET_ALL_INVENTORY_WORKFLOWS_WIP)
            .toPromise()
            .then(response => response.json() as ResponseBase<InventoryWorkflow[]>)
            .catch(HandleError.handle);
    }

    public _addInventoryWorkflows(workflowRequest): Promise<ResponseBase<any>> {
        let body = JSON.stringify(workflowRequest);
        return this.http.post(Global.API_ADD_INVENTORY_WORKFLOW, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }

    public _editInventoryWorkflows(workflowRequest): Promise<ResponseBase<any>> {
        let body = JSON.stringify(workflowRequest);
        return this.http.post(Global.API_UPDATE_INVENTORY_WORKFLOW, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }

    public _deleteInventoryWorkflows(id): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id : id});
        return this.http.post(Global.API_DELETE_INVENTORY_WORKFLOW, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }

    public _getInventoryWorkflowsTypeHeads(request): Promise<ResponseBase<any>> {
        let body = JSON.stringify(request);
        return this.http.post(Global.API_GET_ALL_INVENTORY_WORKFLOWS_HEADS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }

    public _getInventoryWorkflowById(id): Promise<ResponseBase<InventoryWorkflow>> {
        let body = JSON.stringify({ id: id });
        return this.http.post(Global.API_GET_ALL_INVENTORY_WORKFLOW_BY_ID, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<InventoryWorkflow>)
            .catch(HandleError.handle);
    }

    public _addInventoryWorkflowsWIP(workflowRequest): Promise<ResponseBase<any>> {
        let body = JSON.stringify(workflowRequest);
        return this.http.post(Global.API_ADD_INVENTORY_WORKFLOW_WIP, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }

    public _editInventoryWorkflowsWIP(workflowRequest): Promise<ResponseBase<any>> {
        let body = JSON.stringify(workflowRequest);
        return this.http.post(Global.API_EDIT_INVENTORY_WORKFLOW_WIP, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<any>)
            .catch(HandleError.handle);
    }
}

