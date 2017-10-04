import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';

import * as Global from '../global'
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import { BaseSalesOrder, SalesOrder } from '../models/salesorder.model';

@Injectable()
export class SalesOrderService {

    constructor(private http: Http) {
    }

    public _getSalesOrderNumber(): Promise<ResponseBase<any>> {
        return this.http.get(Global.API_GET_SALES_ORDER_NUMBER)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getAllSalesOrders(): Promise<ResponseBase<BaseSalesOrder[]>> {
        return this.http.get(Global.API_GET_ALL_SALES_ORDERS)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getSalesOrderById(salesOrderId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: salesOrderId });
        return this.http.post(Global.API_GET_SALES_ORDER_BY_ID, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _generateSalesOrder(request: SalesOrder): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            salesOrderNumber: request.salesOrderNumber,
            salesOrderSeries: request.salesOrderSeries,
            salesOrderDate: request.salesOrderDate,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            companyAddress: request.tenantInfo.address,
            customerName: request.customerInfo.companyName,
            customerGSTIN: request.customerInfo.gstin,
            billingAddress: request.customerInfo.billingAddress,
            shippingAddress: request.customerInfo.shippingAddress,
            lineItems: request.products,
            totalSGST: request.totalSGST,
            totalCGST: request.totalCGST,
            totalIGST: request.totalIGST,
            totalAmount: request.totalAmount
        });

        return this.http.post(Global.API_GENERATE_SALES_ORDER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _editSalesOrder(request: SalesOrder): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            salesOrderNumber: request.salesOrderNumber,
            salesOrderSeries: request.salesOrderSeries,
            salesOrderDate: request.salesOrderDate,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            companyAddress: request.tenantInfo.address,
            customerName: request.customerInfo.companyName,
            customerGSTIN: request.customerInfo.gstin,
            billingAddress: request.customerInfo.billingAddress,
            shippingAddress: request.customerInfo.shippingAddress,
            lineItems: request.products,
            totalSGST: request.totalSGST,
            totalCGST: request.totalCGST,
            totalIGST: request.totalIGST,
            totalAmount: request.totalAmount
        });

        return this.http.post(Global.API_EDIT_SALES_ORDER, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _deleteSalesOrderById(salesOrderId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: salesOrderId });
        return this.http.post(Global.API_DELETE_SALES_ORDER, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }
}
