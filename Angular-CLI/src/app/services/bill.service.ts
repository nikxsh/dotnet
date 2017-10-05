import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';
import * as Global from '../global'
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import { BillPayment, BaseBill, Bill } from '../models/bill.model';
import { PagingRequest, Payment } from '../Models/common.model';
import { PurchaseReceiveItem } from '../models/purchaseorder.model';

@Injectable()
export class BillService {

    constructor(private http: Http) {
    }

    public _getBillNumber(): Promise<ResponseBase<any>> {
        return this.http.get(Global.API_GET_BILL_NUMBER)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getBillPayments(take, skip): Promise<ResponseBase<BillPayment[]>> {

        let body = JSON.stringify(new PagingRequest(take, skip));

        return this.http.post(Global.API_GET_BILL_PAYMENTS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<BillPayment[]>)
            .catch(HandleError.handle);
    }

    public _getAllBills(): Promise<ResponseBase<BaseBill[]>> {
        return this.http.get(Global.API_GET_ALL_BILLS)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getBillById(billId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: billId });
        return this.http.post(Global.API_GET_BILL_BY_ID, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getPurchaseRecieveItems(purchaseId, receiveNumber): Promise<ResponseBase<PurchaseReceiveItem[]>> {
        let body = JSON.stringify({ Id: purchaseId, receiveNumber: receiveNumber });
        return this.http.post(Global.API_GET_PURCHASE_RECIEVE_ITEMS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<PurchaseReceiveItem[]>)
            .catch(HandleError.handle);
    }

    public _createBill(request: Bill): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            billNumber: request.billNumber,
            billSeries: request.billSeries,
            billDate: request.billDate,
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

        return this.http.post(Global.API_CREATE_BILL, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _createPurchaseReceiveBill(request: Bill): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            billNumber: request.billNumber,
            billSeries: request.billSeries,
            billDate: request.billDate,
            referenceNumber: request.referenceNumber,
            purchaseId: request.purchaseId,
            purchaseReceiveNumber: request.receiveNumber,
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

        return this.http.post(Global.API_CREATE_PURCHASEORDER_BILL, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }


    public _editBill(request: Bill): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            billNumber: request.billNumber,
            billSeries: request.billSeries,
            billDate: request.billDate,
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

        return this.http.post(Global.API_EDIT_BILL, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _editPurchaseReceiveBill(request: Bill): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            billNumber: request.billNumber,
            billSeries: request.billSeries,
            billDate: request.billDate,
            purchaseId: request.purchaseId,
            purchaseReceiveNumber: request.receiveNumber,
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

        return this.http.post(Global.API_EDIT_PURCHASE_RECEIVE_BILL, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _payBillAmount(payment: Payment, billId, purchaseId): Promise<ResponseBase<string>> {

        let body = JSON.stringify({ id: billId, payment: payment, purchaseId: purchaseId });

        var url = purchaseId === undefined || purchaseId === null ? Global.API_PAY_BILL_AMOUNT : Global.API_PAY_PURCHASE_BILL_AMOUNT;

        return this.http.post(url, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _deleteBillById(billId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: billId });
        return this.http.post(Global.API_DELETE_BILL, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _deletePurchaseRecieveBillById(billId, purchaseId, deletedLineItems): Promise<ResponseBase<any>> {

        let body = JSON.stringify({ id: billId, purchaseId: purchaseId, DeleteLineItems: deletedLineItems });

        return this.http.post(Global.API_DELETE_PURCHASE_RECEIVE_BILL, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }
}