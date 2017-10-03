
import { Address, InvoiceStatus, Payment } from "./common.model";
import { Product } from "./product.model";
import { Catalogue } from "./contact.model";
import { Tenant } from "./profile.model";

export class BaseInvoice {
    constructor(
        public id: string = undefined,
        public salesId: string = undefined,
        public invoiceNumber: string = undefined,
        public invoiceSeries: string = undefined,
        public customerName: string = undefined,
        public customerInfo: Catalogue = undefined,
        public status: InvoiceStatus = InvoiceStatus.Open,
        public invoiceDate: string = undefined,
        public payments: Payment[] = [],
        public totalAmount: number = 0) { 
    }
}

export class Invoice extends BaseInvoice {

    constructor(
        public referenceNumber: string = undefined,
        public tenantInfo?: Tenant,
        public invoiceHtml: string = undefined, 
        public products: InvoiceProduct[] = [],
        public totalCGST: number = 0,
        public totalSGST: number = 0,
        public totalIGST: number = 0) {
        super();
    }
}

export class InvoiceProduct extends Product {
    constructor(
        public quantity: number = 0,
        public ordered: number = 0,
        public invoiced: number = 0,
        public rate: number = 0,
        public discount: number = 0,
        public taxable: number = 0,
        public cgstSlab: number = 0,
        public cgstAmount: number = 0,
        public sgstSlab: number = 0,
        public sgstAmount: number = 0,
        public igstSlab: number = 0,
        public igstAmount: number = 0,
        public productTotal: number = 0
    ) {
        super();                      
    }
}

export class InvoicePayment {

    constructor(
        public id: string = undefined,
        public invoiceNumber: string = undefined,            
        public customerName: string = undefined,
        public payments: Payment[] = [],
        public totalAmount: number = 0) {
    }
}      

export class DeletedLineItems {
    constructor(
        public id: string = undefined,
        public quantity: number = undefined) {
    }
}  

export class SalesInvoiceQuantity {
    constructor(
        public id: string = undefined,
        public invoicedQuantity: number = undefined,
        public orderedQuantity: number = undefined) {
    }
} 