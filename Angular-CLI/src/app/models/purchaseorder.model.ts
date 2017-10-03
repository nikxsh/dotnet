import { Catalogue } from "./contact.model";
import { Product } from "./product.model";
import { Tenant } from "./profile.model";

export class BasePurchaseOrder {
    constructor(
        public id: string = undefined,
        public purchaseOrderNumber: string = undefined,
        public purchaseOrderSeries: string = undefined,
        public vendorName: string = undefined,
        public vendorInfo: Catalogue = undefined,
        public status: PurchaseOrderStatus = PurchaseOrderStatus.Open,
        public purchaseReceive: PurchaseReceive[] = [],
        public purchaseOrderDate: string = undefined,
        public totalAmount: number = 0) {
    }
}

export class PurchaseOrder extends BasePurchaseOrder {

    constructor(
        public referenceNumber: number = undefined,
        public tenantInfo: Tenant = undefined,
        public purchaseOrderHtml: string = undefined,
        public products: PurchaseOrderProduct[] = [],
        public totalCGST: number = 0,
        public totalSGST: number = 0,
        public totalIGST: number = 0) {
        super();
    }
}

export class PurchaseOrderProduct extends Product {
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
        public productTotal: number = 0,
        public received: number = 0
    ) {
        super();
    }
}

export class PurchaseReceive {
    constructor(
        public receiveNumber: string = undefined,
        public receivedOnDate: string = undefined,
        public billId: string = undefined,
        public receiveLineItem: PurchaseReceiveItem[] = []
    ) {
    }
}

export class PurchaseReceiveItem {
    constructor(
        public productId: string = undefined,
        public productName: string = undefined,
        public quantity: number = 0,
        public received: number = 0,
        public quantityToReceive: number = 0,
        public rate: number = undefined
    ) {
    }
}

export enum PurchaseOrderStatus {
    Open,
    PartiallyReceived,
    Received,
    Closed
}