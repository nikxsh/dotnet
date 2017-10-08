//core
import { Component, OnInit, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { map } from "rxjs/operator/map";
import { debounceTime } from 'rxjs/operator/debounceTime';
import { distinctUntilChanged } from 'rxjs/operator/distinctUntilChanged';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { ValueObjectPair, Address, MessageHandler, PagingRequest, Filter, ProductPagingRequest } from '../../Models/common.model';
import { Catalogue } from '../../Models/contact.model';
import { Product } from '../../models/product.model';
import { PurchaseOrder, PurchaseOrderProduct } from '../../models/purchaseorder.model';
import { Tenant } from '../../Models/profile.model';
//Services
import { CommonService } from '../../services/common.service';
import { TenantService } from '../../services/tenant.service';
import { ProductService } from '../../services/product.service';
import { PurchaseOrderService } from '../../services/purchaseorder.service';
import { LocalStorageService } from '../../services/storage.service';
//Helper
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';
import { getFormattedDate } from '../../helpers/common.utility';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
    selector: 'app-purchaseorder',
    templateUrl: './purchaseorder.component.html',
    styleUrls: ['./purchaseorder.component.css']
})
export class PurchaseOrderComponent implements OnInit {

    @ViewChild("PurchaseOrderDetailsForm")
    purchaseOrderDetailsForm: FormControl

    @ViewChild("PurchaseOrderItemsForm")
    purchaseOrderItemsForm: FormControl

    private billingAddressModalRef: BsModalRef;

    private vendorDataSource: any;
    private vendorTypeaheadNoResults: boolean;
    private vendorTypeaheadLoading: boolean;

    private productDataSource: any;
    private productTypeaheadNoResults: boolean;
    private productTypeaheadLoading: boolean;
    private selectedProduct: string;

    private title: string = 'Create Purchase Order';
    private lstStates: Array<ValueObjectPair> = [];
    private lstCountries: Array<ValueObjectPair> = [];
    private lstVendorHeads: Array<Catalogue> = [];
    private lstProductHeads: Array<Product> = [];
    private billingAddressModel: Address = new Address();
    private showIGST: boolean = false;
    private isCustomerSelected: boolean = false;
    private purchaseOrderModel: PurchaseOrder;
    private apiMessage: string;
    private purchaseOrderId: string = '';
    private isNewPurchaseOrder: boolean = true;
    private message: MessageHandler = new MessageHandler();
    private productTypes: any[] = Global.Product_Types;

    constructor(
        private commonService: CommonService,
        private tenantService: TenantService,
        private productService: ProductService,
        private purchaseOrderService: PurchaseOrderService,
        private storageService: LocalStorageService,
        private router: Router,
        private route: ActivatedRoute,
        private modalServiceRef: BsModalService) {

        this.vendorDataSource = Observable
            .create((observer: any) => {
                // Runs on every search
                observer.next(this.purchaseOrderModel.vendorName);
            })
            .mergeMap((token: string) => this.getVendorInfo(token));

        this.productDataSource = Observable
            .create((observer: any) => {
                // Runs on every search
                observer.next(this.selectedProduct);
            })
            .mergeMap((token: string) => this.getProductInfo(token));
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.purchaseOrderId = params['id'];

            this.purchaseOrderModel = new PurchaseOrder();
            this.purchaseOrderModel.purchaseOrderDate = getFormattedDate(new Date().toDateString());
            this.purchaseOrderModel.tenantInfo = new Tenant();
            this.purchaseOrderModel.vendorInfo = new Catalogue(new Address());
            this.purchaseOrderModel.products.push(new PurchaseOrderProduct());

            try {
                let tenantInfo = JSON.parse(this.storageService._getTenantInfo()) as Tenant;
                Object.assign(this.purchaseOrderModel.tenantInfo, tenantInfo);

                this.getStates();
                this.getCountries();
                if (this.purchaseOrderId == undefined || this.purchaseOrderId == "") {
                    this.generatePurchaseOrderId();
                }
                else {
                    this.isNewPurchaseOrder = false;
                    this.getPurchaseOrderById(this.purchaseOrderId);
                }
            }
            catch (e) {
                this.message.text = Global.UI_ERROR;
                this.message.type = 2;
            }
        });
    }

    private addItem(flag: boolean) {
        try {
            if (flag)
                if (this.purchaseOrderItemsForm.valid || this.purchaseOrderModel.products.length == 0)
                    this.purchaseOrderModel.products.push(new PurchaseOrderProduct());
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private onRateChange(index) {
        try {
            let product = this.purchaseOrderModel.products[index];
            let amount = this.round(product.quantity * product.rate, 2);
            product.productTotal = amount;
            product.taxable = amount;
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private addDiscount(index) {
        try {
            let product = this.purchaseOrderModel.products[index];
            let total = this.round(product.quantity * product.rate, 2);
            product.productTotal = this.round(total - ((product.discount / 100) * total), 2);
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private addCgst(index) {
        try {
            let product = this.purchaseOrderModel.products[index];
            let total = this.round(product.quantity * product.rate, 2);
            product.cgstAmount = this.round((product.cgstSlab / 100) * total, 2);
            product.productTotal = this.round(total + product.cgstAmount, 2);
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private addSgst(index) {
        try {
            let product = this.purchaseOrderModel.products[index];
            let total = this.round(product.quantity * product.rate, 2);
            product.sgstAmount = this.round((product.sgstSlab / 100) * total, 2);
            product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount, 2);
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private addIgst(index) {
        try {
            let product = this.purchaseOrderModel.products[index];
            let total = this.round(product.quantity * product.rate, 2);
            product.igstAmount = this.round((product.igstSlab / 100) * total, 2);
            product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount + product.igstAmount, 2);
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private getTotal() {

        let total = 0;
        try {
            this.purchaseOrderModel.products.forEach(x => total += x.productTotal);
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
        return Math.round(total);
    }

    private manipulateTaxWindow() {
        try {
            var tenantStateCode = this.purchaseOrderModel.tenantInfo.orgProfile.gstin.substr(0, 2).toUpperCase();
            var contactSateCode = this.purchaseOrderModel.vendorInfo.gstin.substr(0, 2).toUpperCase();

            if (tenantStateCode === contactSateCode)
                this.showIGST = false;
            else
                this.showIGST = true;
        }
        catch (e) {
            this.message.text = "GSTN is missing!";
            this.message.type = 2;
        }
    }

    private getVendorInfo(token): Observable<any> {
        let query = new RegExp(token, 'ig');
        try {
            var request = new PagingRequest(0, 100);
            request.isEnable = true;
            request.filter.push(new Filter("companyname", token));

            this.tenantService._getVendorContacts(request)
                .then(result => {
                    if (result.status == 1) {
                        this.lstVendorHeads = result.data;
                    }
                    else
                        this.apiMessage = result.message;
                },
                error => {
                    HandleError.handle(error);
                });
        }
        catch (e) {
            HandleError.handle(e);
        }

        return Observable.of(this.lstVendorHeads.filter((vendor: Catalogue) => {
            return query.test(vendor.companyName);
        }));
    }

    public changeVendorTypeaheadLoading(e: boolean): void {
        this.vendorTypeaheadLoading = e;
    }

    public changeVendorTypeaheadNoResults(e: boolean): void {
        this.vendorTypeaheadNoResults = e;
    }

    public vendorTypeheadOnSelect(e: TypeaheadMatch): void {
        try {
            this.isCustomerSelected = true;
            var info = (e.item as Catalogue);
            Object.assign(this.purchaseOrderModel.vendorInfo, info);
            Object.assign(this.billingAddressModel, this.purchaseOrderModel.vendorInfo.billingAddress);
            this.purchaseOrderModel.vendorInfo.gstin = info.gstin;
            this.manipulateTaxWindow();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private getProductInfo(token): Observable<any> {
        let query = new RegExp(token, 'ig');
        try {
            var request = new ProductPagingRequest(this.purchaseOrderModel.products.filter(n => n.id != null || n.id != undefined).map(x => x.id));
            request.filter.push(new Filter("name", token));
            request.isEnable = true;

            this.productService._getProducts(request)
                .then(result => {
                    if (result.status == 1)
                        this.lstProductHeads = result.data;
                    else {
                        this.message.text = result.message;
                        this.message.type = 2;
                    }
                },
                error => {
                    HandleError.handle(error);
                });
        }
        catch (e) {
            HandleError.handle(e);
        }

        return Observable.of(this.lstProductHeads.filter((product: Product) => {
            return query.test(product.productName);
        }));
    }

    public changeProuctTypeaheadLoading(e: boolean): void {
        this.productTypeaheadLoading = e;
    }

    public changeProuctTypeaheadNoResults(e: boolean): void {
        this.productTypeaheadNoResults = e;
    }

    public productTypeheadOnSelect(e: TypeaheadMatch, index): void {
        try {
            let product = this.purchaseOrderModel.products[index];
            product.id = e.item.id;
            product.productName = e.item.productName;
            product.productType = e.item.productType;
            product.productCode = e.item.productCode;
            product.uom = e.item.uom;
            product.skuCode = e.item.skuCode;
            if (this.showIGST) {
                product.igstSlab = e.item.taxSlab;
            }
            else {
                product.cgstSlab = e.item.taxSlab / 2;
                product.sgstSlab = e.item.taxSlab / 2;
            }
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private generatePurchaseOrderId() {
        try {
            this.purchaseOrderService._getPurchaseOrderNumber()
                .then(result => {
                    if (result.status == 1) {
                        this.purchaseOrderModel.purchaseOrderNumber = result.data.newPurchaseOrderNumber;
                        this.purchaseOrderModel.purchaseOrderSeries = result.data.newPurchaseOrderSeries;
                    }
                    else {
                        this.message.text = result.message;
                        this.message.type = 2;
                    }
                },
                error => {
                    this.message.text = Global.UI_ERROR;
                    this.message.type = 2;
                });
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private getPurchaseOrderById(PurchaseOrderId) {
        try {
            this.purchaseOrderService._getPurchaseOrderById(PurchaseOrderId)
                .then(result => {
                    if (result.status == 1) {
                        this.purchaseOrderModel.id = result.data.id;
                        this.purchaseOrderModel.purchaseOrderNumber = result.data.purchaseOrderNumber;
                        this.purchaseOrderModel.purchaseOrderDate = getFormattedDate(result.data.purchaseOrderDate);
                        this.purchaseOrderModel.products = result.data.lineItems;

                        this.purchaseOrderModel.vendorInfo = new Catalogue(new Address());
                        this.purchaseOrderModel.vendorInfo.gstin = result.data.vendorGSTIN;
                        this.purchaseOrderModel.vendorInfo.companyName = result.data.vendorName;
                        this.purchaseOrderModel.vendorInfo.billingAddress = result.data.billingAddress;

                        Object.assign(this.billingAddressModel, this.purchaseOrderModel.vendorInfo.billingAddress);

                        this.purchaseOrderModel.totalCGST = result.data.totalCGST;
                        this.purchaseOrderModel.totalSGST = result.data.totalSGST;
                        this.purchaseOrderModel.totalIGST = result.data.totalIGST;
                        this.purchaseOrderModel.totalAmount = result.data.totalAmount;

                        this.manipulateTaxWindow();

                        if (this.purchaseOrderModel.vendorInfo.companyName != undefined || this.purchaseOrderModel.vendorInfo.companyName != "")
                            this.isCustomerSelected = true;
                    }
                    else {
                        this.message.text = result.message;
                        this.message.type = 2;
                    }
                },
                error => {
                    this.message.text = Global.UI_ERROR;
                    this.message.type = 2;
                });
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private removeItem(index) {
        try {
            this.purchaseOrderModel.products.splice(index, 1);
            this.purchaseOrderModel.totalAmount = this.getTotal();
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private updateBillingAddress() {
        try {
            Object.assign(this.purchaseOrderModel.vendorInfo.billingAddress, this.billingAddressModel);
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
        this.billingAddressModalRef.hide();
    }

    private savePurchaseOrder() {
        try {
            var totalCGST: number = 0, totalSGST: number = 0, totalIGST: number = 0;
            this.purchaseOrderModel.products.forEach(x => { totalCGST += x.cgstAmount, totalSGST += x.sgstAmount, totalIGST += x.igstAmount });
            this.purchaseOrderModel.totalCGST = totalCGST;
            this.purchaseOrderModel.totalSGST = totalSGST;
            this.purchaseOrderModel.totalIGST = totalIGST;

            this.message.text = "Saving in progress...";
            this.message.type = 1;

            if (this.isNewPurchaseOrder) {
                this.purchaseOrderService._createPurchaseOrder(this.purchaseOrderModel, this.isNewPurchaseOrder)
                    .then(result => {
                        if (result.status == 1) {
                            this.message.text = '';
                            this.router.navigate(['app/main/purchaseorders']);
                        }
                        else {
                            this.message.text = result.message;
                            this.message.type = 2;
                        }
                    },
                    error => {
                        this.message.text = Global.UI_ERROR;
                        this.message.type = 2;
                    });
            }
            else {
                this.purchaseOrderService._editPurchaseOrder(this.purchaseOrderModel)
                    .then(result => {
                        if (result.status == 1) {
                            this.message.text = '';
                            this.router.navigate(['app/main/purchaseorders']);
                        }
                        else {
                            this.message.text = result.message;
                            this.message.type = 2;
                        }
                    },
                    error => {
                        this.message.text = Global.UI_ERROR;
                        this.message.type = 2;
                    });

            }
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private getStates() {
        try {
            this.commonService._getStates()
                .subscribe(data => {
                    this.lstStates = data;
                },
                error => {
                    this.message.text = Global.UI_ERROR;
                    this.message.type = 2;
                });
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private getCountries() {
        try {
            this.commonService._getCountries()
                .subscribe(data => {
                    this.lstCountries = data;
                },
                error => {
                    this.message.text = Global.UI_ERROR;
                    this.message.type = 2;
                });
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    private round(value, precision) {
        try {
            let multiplier = Math.pow(10, precision || 0);
            return Math.round(value * multiplier) / multiplier;
        }
        catch (e) {
            this.message.text = Global.UI_ERROR;
            this.message.type = 2;
        }
    }

    public openModal(template: TemplateRef<any>) {
        try {
            this.billingAddressModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
        }
        catch (e) {
            HandleError.handle(e);
        }
    }
}
