
export class Product {

    constructor(
        public id?: string,
        public productName?: string,
        public description?: string,
        public productType?: ProductType,
        public productCode?: string,
        public skuCode?: string,
        public taxSlab?: number,
        public uom?: string,
        public categoryId?: string,
        public categoryName?: string,
        public isEnable: boolean = true,
        public openingStock?: number
    ) {

    }
}

export class ProductCategory {

    constructor(
        public id?: string,       
        public name?: string,
        public isEnable: boolean = true,
        public show: boolean = true
    ) { }    
}

export enum ProductType {
    Goods,
    Services
}