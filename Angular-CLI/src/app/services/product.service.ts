import { Injectable } from '@angular/core'
import { Http, Headers, Response, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/toPromise';

import * as Global from '../global'
import { Product, ProductCategory } from '../models/product.model';
import { ResponseBase } from '../helpers/adapter.utility';
import { HandleError } from '../helpers/error.utility';

@Injectable()
export class ProductService {

    constructor(private http: Http) {
    }

    public _addProduct(request: Product): Promise<ResponseBase<Product>> {

        let body = JSON.stringify(request);

        return this.http.post(Global.API_ADD_PRODUCT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product>)
            .catch(HandleError.handle);
    }

    public _editProduct(request: Product): Promise<ResponseBase<Product>> {

        let body = JSON.stringify(request);

        return this.http.post(Global.API_EDIT_PRODUCT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product>)
            .catch(HandleError.handle);
    }

    public _enableDisableProduct(isEnable: boolean, productID: string): Promise<ResponseBase<Product>> {

        let body = JSON.stringify({ id: productID, IsEnable: isEnable });

        return this.http.post(Global.API_ENABLE_DISABLE_PRODUCT, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product>)
            .catch(HandleError.handle);
    }

    public _enableDisableProductCategory(isEnable: boolean, productCategoryID: string): Promise<ResponseBase<ProductCategory>> {

        let body = JSON.stringify({ id: productCategoryID, IsEnable: isEnable });

        return this.http.post(Global.API_ENABLE_DISABLE_PRODUCT_CATEGORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<ProductCategory>)
            .catch(HandleError.handle);
    }

    public _addProductCategory(request: ProductCategory): Promise<ResponseBase<ProductCategory>> {

        let body = JSON.stringify(request);

        return this.http.post(Global.API_ADD_PRODUCT_CATEGORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<ProductCategory>)
            .catch(HandleError.handle);
    }

    public _renameProductCategory(request: ProductCategory): Promise<ResponseBase<ProductCategory>> {

        let body = JSON.stringify(request);

        return this.http.post(Global.API_EDIT_PRODUCT_CATEGORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<ProductCategory>)
            .catch(HandleError.handle);
    }

    public _getProducts(request): Promise<ResponseBase<Product[]>> {

        let body = JSON.stringify(request);

        return this.http.post(Global.API_GET_ALL_PRODUCTS, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product[]>)
            .catch(HandleError.handle);
    }

    public _getProductsByCategoryId(categoryId): Promise<ResponseBase<Product[]>> {
        let body = JSON.stringify({ id: categoryId });     
        return this.http.post(Global.API_GET_PRODUCTS_OF_CATEGORY, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product[]>)
            .catch(HandleError.handle);
    }

    public _getProductById(productId): Promise<ResponseBase<Product>> {
        let body = JSON.stringify({ id: productId });
        return this.http.post(Global.API_GET_PRODUCT_BY_ID, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<Product>)
            .catch(HandleError.handle);
    }

    public _getProductCategories(IsEnable : boolean): Promise<ResponseBase<ProductCategory[]>> {
        let body = JSON.stringify({ isEnable: IsEnable });
        return this.http.post(Global.API_GET_ALL_PRODUCT_CATEGORIES, body)
            .toPromise()
            .then(response => response.json() as ResponseBase<ProductCategory[]>)
            .catch(HandleError.handle);
    }

}