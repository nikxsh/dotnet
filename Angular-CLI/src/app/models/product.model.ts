
export class Product {

    constructor(
        public id: string = undefined,
        public productName: string = undefined,
        public description: string = undefined,
        public productType: ProductType = 0,
        public productCode: string = undefined,
        public skuCode: string = undefined,
        public taxSlab: number = undefined,
        public uom: string = undefined,
        public categoryId: string = undefined,
        public categoryName: string = undefined,
        public isEnable: boolean = true,
        public openingStock: number = undefined
    ) {

    }
}

export class ProductCategory {

    constructor(
        public id: string = undefined,       
        public name: string = undefined,
        public isEnable: boolean = true,
        public show: boolean = true
    ) { }    
}

export enum ProductType {
    Goods,
    Services
}