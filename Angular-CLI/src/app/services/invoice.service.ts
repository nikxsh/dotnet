import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';

import * as Global from '../global'
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';
import { InvoicePayment, BaseInvoice, SalesInvoiceQuantity, Invoice } from '../models/invoice.model';
import { PagingRequest, Payment } from '../Models/common.model';

@Injectable()
export class InvoiceService {

    constructor(private http: Http) {
    }

    public _getInvoiceNumber(): Promise<ResponseBase<any>> {
        return this.http.get(Global.API_GET_INVOICE_NUMBER)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getInvoicePayments(take, skip): Promise<ResponseBase<InvoicePayment[]>> {

        let body = JSON.stringify(new PagingRequest(take, skip));

        return this.http.post(Global.API_GET_INVOICE_PAYMENTS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<InvoicePayment[]>)
            .catch(HandleError.handle);
    }

    public _getAllInvoices(): Promise<ResponseBase<BaseInvoice[]>> {
        return this.http.get(Global.API_GET_ALL_INVOICES)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getInvoiceById(invoiceId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: invoiceId });
        return this.http.post(Global.API_GET_INVOICE_BY_ID, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _getInvoiceItemsQuantity(salesId): Promise<ResponseBase<SalesInvoiceQuantity[]>> {
        let body = JSON.stringify({ id: salesId });
        return this.http.post(Global.API_GET_INVOICE_ITEM_QUANTITY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<SalesInvoiceQuantity[]>)
            .catch(HandleError.handle);
    }

    public _getSalesInvoiceById(saleId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: saleId });
        return this.http.post(Global.API_GET_SALES_INVOICE_BY_ID, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _createInvoice(request: Invoice): Promise<ResponseBase<string>> {  

        let body = JSON.stringify({
            id: request.id,
            invoiceNumber: request.invoiceNumber,
            invoiceSeries: request.invoiceSeries,
            invoiceDate: request.invoiceDate,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            bankName: request.tenantInfo.orgProfile.bankName,
            accountNumber: request.tenantInfo.orgProfile.accountNumber,
            iFSCCode: request.tenantInfo.orgProfile.ifscCode,
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
                                                                                       
        return this.http.post(Global.API_CREATE_INVOICE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _createSalesInvoice(request: Invoice): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            invoiceNumber: request.invoiceNumber,
            invoiceSeries: request.invoiceSeries,
            invoiceDate: request.invoiceDate,
            saleId: request.salesId,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            bankName: request.tenantInfo.orgProfile.bankName,
            accountNumber: request.tenantInfo.orgProfile.accountNumber,
            iFSCCode: request.tenantInfo.orgProfile.ifscCode,
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

        return this.http.post(Global.API_CREATE_SALES_INVOICE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }


    public _editInvoice(request: Invoice): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            invoiceNumber: request.invoiceNumber,
            invoiceSeries: request.invoiceSeries,
            invoiceDate: request.invoiceDate,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            bankName: request.tenantInfo.orgProfile.bankName,
            accountNumber: request.tenantInfo.orgProfile.accountNumber,
            iFSCCode: request.tenantInfo.orgProfile.ifscCode,
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
                                                                                         
        return this.http.post(Global.API_EDIT_INVOICE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }  

    public _editSalesInvoice(request: Invoice): Promise<ResponseBase<string>> {

        let body = JSON.stringify({
            id: request.id,
            invoiceNumber: request.invoiceNumber,
            invoiceSeries: request.invoiceSeries,
            invoiceDate: request.invoiceDate,
            saleId: request.salesId,
            referenceNumber: request.referenceNumber,
            company: {
                "name": request.tenantInfo.orgProfile.tenantName,
                "email": request.tenantInfo.orgProfile.emailAddress,
                "gstin": request.tenantInfo.orgProfile.gstin,
                "phone": request.tenantInfo.address.phoneNumber,
            },
            bankName: request.tenantInfo.orgProfile.bankName,
            accountNumber: request.tenantInfo.orgProfile.accountNumber,
            iFSCCode: request.tenantInfo.orgProfile.ifscCode,
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

        return this.http.post(Global.API_EDIT_SALES_INVOICE, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }

    public _deleteInvoiceById(invoiceId): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: invoiceId });
        return this.http.post(Global.API_DELETE_INVOICE, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _deleteSalesInvoiceById(invoiceId, salesId, deletedLineItems): Promise<ResponseBase<any>> {
        let body = JSON.stringify({ id: invoiceId, salesId: salesId, DeleteLineItems: deletedLineItems });
        return this.http.post(Global.API_DELETE_SALES_INVOICE, body)
            .toPromise()
            .then(response => response.json())
            .catch(HandleError.handle);
    }

    public _payInvoiceAmount(payment: Payment, invoiceId, salesId): Promise<ResponseBase<string>> {

        let body = JSON.stringify({ id: invoiceId, payment: payment, salesId: salesId });

        var url = salesId === undefined || salesId === null  ? Global.API_PAY_INVOICE_AMOUNT : Global.API_PAY_SALES_INVOICE_AMOUNT;

        return this.http.post(url, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<string>)
            .catch(HandleError.handle);
    }     
}