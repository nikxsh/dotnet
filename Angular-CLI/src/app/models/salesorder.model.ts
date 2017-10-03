import { Address, InvoiceStatus } from "./common.model";
import { Product } from "./product.model";
import { Catalogue } from "./contact.model";
import { Tenant } from "./profile.model";

export class BaseSalesOrder {
    constructor(
        public id: string = undefined,
        public salesOrderNumber: string = undefined,
        public salesOrderSeries: string = undefined,
        public customerName: string = undefined,
        public customerInfo: Catalogue = undefined,
        public salesOrderStatus: SalesOrderStatus = SalesOrderStatus.Open,
        public salesOrderDate: string = undefined,
        public invoices: SalesOrderInvoice[] = [],
        public totalAmount: number = 0) {
    }
}

export class SalesOrder extends BaseSalesOrder {

    constructor(
        public tenantInfo: Tenant = undefined,
        public salesOrderHtml: string = undefined,
        public products: SalesOrderProduct[] = [],
        public totalCGST: number = 0,
        public totalSGST: number = 0,
        public totalIGST: number = 0) {
        super();
    }
}

export class SalesOrderProduct extends Product {
    constructor(
        public quantity: number = 0,
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

export class SalesOrderInvoice {
    constructor(
        public invoiceId?: string,
        public invoiceNumber?: string,
        public invoiceDate?: Date,
        public invoiceStatus?: InvoiceStatus,
        public invoiceAmount?: number) {
    }
}

export enum SalesOrderStatus {
    Open,
    PartiallyInvoiced,
    Invoiced,
    Closed
}