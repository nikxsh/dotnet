import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';
import * as Global from '../global'
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import { BasePurchaseOrder, PurchaseOrder, PurchaseReceive } from '../models/purchaseorder.model';

@Injectable()
export class PurchaseOrderService {

    constructor(private http: Http) {
    }

    public _getPurchaseOrderNumber(): Promise<ResponseBase<any>> {
        return this.http.get(Global.API_GET_PURCHASEORDER_NUMBER)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }    

    public _getAllPurchaseOrders(): Promise<ResponseBase<BasePurchaseOrder[]>> {
        return this.http.get(Global.API_GET_ALL_PURCHASEORDERS)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getPurchaseOrderById(purchaseOrderId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: purchaseOrderId });
        return this.http.post(Global.API_GET_PURCHASEORDER_BY_ID, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _createPurchaseOrder(request: PurchaseOrder, isNewPurchaseOrder: boolean): Promise<ResponseBase<string>> {  

        let body = JSON.stringify({
            purchaseOrderNumber: request.purchaseOrderNumber,
            purchaseOrderSeries: request.purchaseOrderSeries,
            purchaseOrderDate: request.purchaseOrderDate,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            companyAddress: request.tenantInfo.address,
            vendorName: request.vendorInfo.companyName,
            vendorGSTIN: request.vendorInfo.gstin,
            billingAddress: request.vendorInfo.billingAddress,
            lineItems: request.products,
            totalSGST: request.totalSGST,
            totalCGST: request.totalCGST,
            totalIGST: request.totalIGST,
            totalAmount: request.totalAmount
        });

        return this.http.post(Global.API_CREATE_PURCHASEORDER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }
    public _editPurchaseOrder(request: PurchaseOrder): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            purchaseOrderNumber: request.purchaseOrderNumber,
            purchaseOrderSeries: request.purchaseOrderSeries,
            purchaseOrderDate: request.purchaseOrderDate,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            companyAddress: request.tenantInfo.address,
            vendorName: request.vendorInfo.companyName,
            vendorGSTIN: request.vendorInfo.gstin,
            billingAddress: request.vendorInfo.billingAddress,
            lineItems: request.products,
            totalSGST: request.totalSGST,
            totalCGST: request.totalCGST,
            totalIGST: request.totalIGST,
            totalAmount: request.totalAmount
        });

        return this.http.post(Global.API_EDIT_PURCHASEORDER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _deletePurchaseOrder(purchaseOrderId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: purchaseOrderId });
        return this.http.post(Global.API_DELETE_PURCHASEORDER, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _addPurchaseReceive(purchaseOrderId, purchaseReceive: PurchaseReceive): Promise<ResponseBase<any>> {

        purchaseReceive.receiveLineItem.forEach(x => { x.received = undefined; x.quantity = undefined, x.productName = undefined })
        let body = JSON.stringify({ id: purchaseOrderId, receive: purchaseReceive });
        return this.http.post(Global.API_ADD_PURCHASE_RECEIVE, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _removePurchaseReceive(purchaseOrderId, purchaseReceive: PurchaseReceive): Promise<ResponseBase<any>> {
        purchaseReceive.receiveLineItem.forEach(x => { x.received = undefined; x.quantity = undefined })
        let body = JSON.stringify({ id: purchaseOrderId, purchaseReceiveNumber: purchaseReceive.receiveNumber, receiveLineItem: purchaseReceive.receiveLineItem });
        return this.http.post(Global.API_REMOVE_PURCHASE_RECEIVE, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }
}