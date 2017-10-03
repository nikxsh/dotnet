
import { Address, Payment, BillStatus } from "./common.model";
import { Product } from "./product.model";
import { Catalogue } from "./contact.model";
import { Tenant } from "./profile.model";

export class BaseBill {
    constructor(
        public id: string = undefined,
        public billNumber: string = undefined,
        public billSeries: string = undefined,
        public purchaseId: string = undefined,
        public receiveNumber: string = undefined,
        public vendorName: string = undefined,
        public vendorInfo: Catalogue = undefined,
        public status: BillStatus = BillStatus.Open,
        public billDate: string = undefined,
        public payments: Payment[] = [],
        public totalAmount: number = 0) {
    }
}

export class Bill extends BaseBill {

    constructor(
        public referenceNumber: number = undefined,
        public billHtml: string = undefined,
        public tenantInfo: Tenant = undefined,
        public products: BillProduct[] = [],
        public totalCGST: number = 0,
        public totalSGST: number = 0,
        public totalIGST: number = 0) {
        super();
    }
}

export class BillProduct extends Product {
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

export class BillPayment {

    constructor(
        public id?: string,
        public billNumber?: string,
        public vendorName?: string,
        public payments: Payment[] = [],
        public totalAmount: number = 0) {
    }
}    