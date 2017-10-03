export class Address {
    constructor(
        public addressLine?: string,
        public city?: string,
        public state?: string,
        public postalCode?: string,
        public phoneNumber?: string,
        public country?: string) {
    }
}

export class PagingRequest {

    constructor(
        public take?: number,
        public skip?: number,
        public filter: Filter[] = [],
        public isEnable: boolean = false,) {
    }
}

export class ProductPagingRequest extends PagingRequest {
    constructor(
        public existingProductIds: string[] = []) {
        super();
    }
}

export class InventoryPagingRequest extends PagingRequest {
    constructor(
        public queryType: number = 0,
        public month: number = 0,
        public year: number = 0,
        public selectedDate: string = undefined,
        public dateRangeFrom: string = undefined,
        public dateRangeTo: string = undefined,
        public id: string = undefined
        ) {
        super();
    }
}

export class Filter {
    constructor(
        public field: string,
        public value: string) {
    }
}

export class GetInfo<T>{
    constructor(
        public id?: string,
        public user?: T,
        public count?: number) {
    }
}

export class ValueObjectPair {    

    constructor(
        public code?: string,
        public name?: string) {
    }
}


export class KeyValuePair {

    constructor(
        public key?: number,
        public value?: string) {
    }
}


export class MessageHandler {
    constructor(
        public text: string = '',
        public type: MessageType = MessageType.Success,
        public isGlobal: boolean = true
    )
    {}
}

enum MessageType { Success = 1, Error = 2, Warning = 3 }

export enum InvoiceStatus {
    Open,
    PartiallyPaid,
    Paid
}

export enum BillStatus {
    Open,
    PartiallyPaid,
    Paid
}

export class Payment {
    constructor(
        public paymentId: string = undefined,
        public referenceNumber: string = undefined,
        public paymentMode: PaymentMode = PaymentMode.Cash,
        public paymentDate: string = undefined,
        public amount: number = 0,
        public status: PaymentStatus = PaymentStatus.Open) {
    }
}  

export enum PaymentStatus { Open, PartiallyPaid, Paid }
export enum PaymentMode { Cash, Cheque, Draft, Card }