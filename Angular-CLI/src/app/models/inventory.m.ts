export class BaseInventory {
    constructor(
        public id: string = undefined,
        public productCode: string = undefined,
        public productName: string = undefined,
        public _in: number = undefined,
        public out: number = undefined,
        public wip: number = undefined,
        public available: number = undefined,
        public uom: string = undefined,
        public date: string = undefined,
        public runningTotal: number = undefined) {

    }
}

export class Inventory extends BaseInventory {
    constructor(
        public invoiceNumber: string = undefined,
        public salesNumber: string = undefined,
        public billNumber: string = undefined,
        public purchaseNumber: string = undefined,
        public purchaseReceiveNumber: string = undefined) {
        super();
    }
}

export class InventoryWorkflow {
    constructor(
        public id: string = undefined,
        public name: string = undefined,
        public inputProducts: Stock[] = [],
        public outputProducts: Stock[] = []) {
    }
}

export class Stock {
    constructor(
        public id: string = undefined,
        public name: string = undefined,
        public code: string = undefined,
        public quantity: number = undefined,
        public uom: string = undefined) {
    }
}

export class FilterSelection {
    constructor(
        public month: string = undefined,
        public year: string = undefined,
        public byDate: string = undefined,
        public fromDate: string = undefined,
        public toDate: string = undefined) {

    }
}

export enum FilterType { ByMonthAndYear, ByDate, ByToAndFromDate }