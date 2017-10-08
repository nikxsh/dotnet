//core
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
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
import { Bill, BillProduct } from '../../models/bill.model';
import { Tenant } from '../../Models/profile.model';
//Services
import { CommonService } from '../../services/common.service';
import { TenantService } from '../../services/tenant.service';
import { ProductService } from '../../services/product.service';
import { BillService } from '../../services/bill.service';
import { PurchaseOrderService } from '../../services/purchaseorder.service';
import { LocalStorageService } from '../../services/storage.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime, getFormattedDate } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html',
  styleUrls: ['./bill.component.css']
})
export class BillComponent implements OnInit {

  @ViewChild("BillDetailsForm")
  BillDetailsForm: FormControl

  @ViewChild("BillItemsForm")
  BillItemsForm: FormControl

  private billingAddressModalRef: BsModalRef;

  private vendorDataSource: any;
  private vendorTypeaheadNoResults: boolean;
  private vendorTypeaheadLoading: boolean;

  private productDataSource: any;
  private productTypeaheadNoResults: boolean;
  private productTypeaheadLoading: boolean;
  private selectedProduct: string;

  private title: string = 'Create Bill';
  private lstStates: Array<ValueObjectPair> = [];
  private lstCountries: Array<ValueObjectPair> = [];
  private lstVendorHeads: Array<Catalogue> = [];
  private lstProductHeads: Array<Product> = [];
  private billingAddressModel: Address = new Address();
  private showIGST: boolean = false;
  private isVendorSelected: boolean = false;
  private billModel: Bill;
  private apiMessage: string;
  private isNewBill: boolean = true;
  private message: MessageHandler = new MessageHandler();
  private productTypes: any[] = Global.Product_Types;
  private isDisabled: boolean = true;
  private isPurchaseRecieveBill: boolean = false;

  constructor(
    private commonService: CommonService,
    private tenantService: TenantService,
    private productService: ProductService,
    private BillService: BillService,
    private purchaseOrderService: PurchaseOrderService,
    private storageService: LocalStorageService,
    private router: Router,
    private route: ActivatedRoute,
    private modalServiceRef: BsModalService) {

    this.vendorDataSource = Observable
      .create((observer: any) => {
        // Runs on every search
        observer.next(this.billModel.vendorName);
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
      let billId = params['id'];
      let purchaseId = params['purchaseId'];
      let recieveNumber = params['recieveNumber'];

      this.isPurchaseRecieveBill = false;
      this.billModel = new Bill();
      this.billModel.billDate = getFormattedDate(new Date().toDateString());
      this.billModel.tenantInfo = new Tenant();
      this.billModel.vendorInfo = new Catalogue(new Address());

      try {
        let tenantInfo = JSON.parse(this.storageService._getTenantInfo()) as Tenant;
        Object.assign(this.billModel.tenantInfo, tenantInfo);

        this.getStates();
        this.getCountries();

        if (billId != undefined && purchaseId != undefined && recieveNumber != undefined) {
          //Edit bill created from purchase order 
          this.isPurchaseRecieveBill = true;
          this.isNewBill = false;
          this.isDisabled = true;
          this.billModel.purchaseId = purchaseId;
          this.billModel.receiveNumber = recieveNumber;
          this.billModel.id = billId;
          this.getBillById(billId);
        }
        else if (billId != undefined) {
          //Edit bill
          this.isNewBill = false;
          this.isDisabled = false;
          this.isPurchaseRecieveBill = false;
          this.billModel.id = billId;
          this.getBillById(billId);
        }
        else if (purchaseId != undefined && recieveNumber != undefined) {
          //Create bill from purchase order
          this.isPurchaseRecieveBill = true;
          this.billModel.purchaseId = purchaseId;
          this.billModel.receiveNumber = recieveNumber;
          this.getPurchaseOrderById();
          this.generateBillId();
        }
        else {
          //Create bill
          this.generateBillId();
          this.billModel.products.push(new BillProduct());
        }
      }
      catch (e) {
        HandleError.handle(e);
      }
    });
  }

  private addItem(flag: boolean) {
    try {
      if (flag && (this.billModel.purchaseId == undefined && this.billModel.receiveNumber == undefined))
        if (this.BillItemsForm.valid || this.billModel.products.length == 0)
          this.billModel.products.push(new BillProduct());
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private onRateChange(index) {
    try {
      let product = this.billModel.products[index];
      let amount = this.round(product.quantity * product.rate, 2);
      product.productTotal = amount;
      product.taxable = amount;
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addDiscount(index) {
    try {
      let product = this.billModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.productTotal = this.round(total - ((product.discount / 100) * total), 2);
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addCgst(index) {
    try {
      let product = this.billModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.cgstAmount = this.round((product.cgstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount, 2);
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addSgst(index) {
    try {
      let product = this.billModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.sgstAmount = this.round((product.sgstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount, 2);
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addIgst(index) {
    try {
      let product = this.billModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.igstAmount = this.round((product.igstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount + product.igstAmount, 2);
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getTotal() {

    let total = 0;
    try {
      this.billModel.products.forEach(x => total += x.productTotal);
    }
    catch (e) {
      HandleError.handle(e);
    }
    return Math.round(total);
  }

  private manipulateTaxWindow() {
    try {
      var tenantStateCode = this.billModel.tenantInfo.orgProfile.gstin.substr(0, 2).toUpperCase();
      var contactSateCode = this.billModel.vendorInfo.gstin.substr(0, 2).toUpperCase();

      if (tenantStateCode === contactSateCode)
        this.showIGST = false;
      else
        this.showIGST = true;
    }
    catch (e) {
      HandleError.handle(e);
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
      if (!this.isPurchaseRecieveBill)
        this.isDisabled = false;
      else
        this.isDisabled = true;

      var info = (e.item as Catalogue);
      Object.assign(this.billModel.vendorInfo, info);
      Object.assign(this.billingAddressModel, this.billModel.vendorInfo.billingAddress);
      this.billModel.vendorInfo.gstin = info.gstin;
      this.manipulateTaxWindow();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
  
  private getProductInfo(token): Observable<any> {
    let query = new RegExp(token, 'ig');
    try {
      var request = new ProductPagingRequest(this.billModel.products.filter(n => n.id != null || n.id != undefined).map(x => x.id));
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
      let product = this.billModel.products[index];
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
      HandleError.handle(e);
    }
  }

  private generateBillId() {
    try {
      this.BillService._getBillNumber()
        .then(result => {
          if (result.status == 1) {
            this.billModel.billNumber = result.data.newBillNumber;
            this.billModel.billSeries = result.data.newBillSeries;
          }
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
  }

  private getBillById(BillId) {
    try {
      this.BillService._getBillById(BillId)
        .then(result => {
          if (result.status == 1) {
            this.billModel.id = result.data.id;
            this.billModel.billNumber = result.data.billNumber;
            this.billModel.billDate = result.data.billDate;
            this.billModel.referenceNumber = result.data.referenceNumber;
            this.billModel.purchaseId = result.data.purchaseId;
            this.billModel.receiveNumber = result.data.receiveNumber;

            this.billModel.billDate = getFormattedDate(result.data.billDate);

            this.billModel.products = result.data.lineItems;

            this.billModel.vendorInfo = new Catalogue(new Address());
            this.billModel.vendorInfo.gstin = result.data.vendorGSTIN;
            this.billModel.vendorInfo.companyName = result.data.vendorName;
            this.billModel.vendorInfo.billingAddress = result.data.billingAddress;

            Object.assign(this.billingAddressModel, this.billModel.vendorInfo.billingAddress);

            this.billModel.totalCGST = result.data.totalCGST;
            this.billModel.totalSGST = result.data.totalSGST;
            this.billModel.totalIGST = result.data.totalIGST;
            this.billModel.totalAmount = result.data.totalAmount;

            this.manipulateTaxWindow();

            if (this.billModel.vendorInfo.companyName != undefined || this.billModel.vendorInfo.companyName != "")
              this.isVendorSelected = true;
          }
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
  }

  private removeItem(index) {
    try {
      this.billModel.products.splice(index, 1);
      this.billModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private updateBillingAddress() {
    try {
      Object.assign(this.billModel.vendorInfo.billingAddress, this.billingAddressModel);
    }
    catch (e) {
      HandleError.handle(e);
    }
    this.billingAddressModalRef.hide();
  }

  private saveBill() {
    try {
      var totalCGST: number = 0, totalSGST: number = 0, totalIGST: number = 0;
      this.billModel.products.forEach(x => { totalCGST += x.cgstAmount, totalSGST += x.sgstAmount, totalIGST += x.igstAmount });
      this.billModel.totalCGST = totalCGST;
      this.billModel.totalSGST = totalSGST;
      this.billModel.totalIGST = totalIGST;

      this.message.text = "Saving in progress...";
      this.message.type = 1;

      if (this.isNewBill) {
        if (!this.isPurchaseRecieveBill) {
          this.BillService._createBill(this.billModel)
            .then(result => {
              if (result.status == 1) {
                this.message.text = '';
                this.router.navigate(['app/main/bills']);
              }
              else {
                this.message.text = result.message;
                this.message.type = 2;
              }
            },
            error => {
              HandleError.handle(error);
            });
        }
        else {
          this.BillService._createPurchaseReceiveBill(this.billModel)
            .then(result => {
              if (result.status == 1) {
                this.message.text = '';
                this.router.navigate(['app/main/bills']);
              }
              else {
                this.message.text = result.message;
                this.message.type = 2;
              }
            },
            error => {
              HandleError.handle(error);
            });
        }
      }
      else {
        if (!this.isPurchaseRecieveBill) {
          this.BillService._editBill(this.billModel)
            .then(result => {
              if (result.status == 1) {
                this.message.text = '';
                this.router.navigate(['app/main/bills']);
              }
              else {
                this.message.text = result.message;
                this.message.type = 2;
              }
            },
            error => {
              HandleError.handle(error);
            });

        }
        else {
          this.BillService._editPurchaseReceiveBill(this.billModel)
            .then(result => {
              if (result.status == 1) {
                this.message.text = '';
                this.router.navigate(['app/main/bills']);
              }
              else {
                this.message.text = result.message;
                this.message.type = 2;
              }
            },
            error => {
              HandleError.handle(error);
            });
        }
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getPurchaseOrderById() {
    try {
      this.purchaseOrderService._getPurchaseOrderById(this.billModel.purchaseId)
        .then(result => {
          if (result.status == 1) {
            this.billModel.referenceNumber = result.data.purchaseOrderNumber;
            this.billModel.vendorInfo = new Catalogue(new Address());
            this.billModel.vendorInfo.gstin = result.data.vendorGSTIN;
            this.billModel.vendorInfo.companyName = result.data.vendorName;
            this.billModel.vendorInfo.billingAddress = result.data.billingAddress;

            Object.assign(this.billingAddressModel, this.billModel.vendorInfo.billingAddress);

            this.BillService._getPurchaseRecieveItems(this.billModel.purchaseId, this.billModel.receiveNumber)
              .then(result => {
                if (result.status == 1) {

                  //Manipulate Tax Window
                  this.manipulateTaxWindow();

                  //Get Products info
                  result.data.forEach(x => {
                    this.productService._getProductById(x.productId)
                      .then(result => {
                        if (result.status == 1) {
                          let product = new BillProduct();
                          product.quantity = x.quantity;
                          product.id = result.data.id;
                          product.productName = result.data.productName;
                          product.productType = result.data.productType;
                          product.productCode = result.data.productCode;
                          product.skuCode = result.data.skuCode;
                          product.uom = result.data.uom;
                          product.taxSlab = result.data.taxSlab;
                          product.rate = x.rate;

                          if (this.showIGST) {
                            product.igstSlab = product.taxSlab;
                          }
                          else {
                            product.cgstSlab = product.taxSlab / 2;
                            product.sgstSlab = product.taxSlab / 2;
                          }

                          this.billModel.products.push(product);
                        }
                      },
                      error => {
                        HandleError.handle(error);
                      });
                  });
                }
                else {
                  this.message.text = result.message;
                  this.message.type = 2;
                }
              },
              error => {
                HandleError.handle(error);
              });
          }
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
  }

  private getStates() {
    try {
      this.commonService._getStates()
        .subscribe(data => {
          this.lstStates = data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getCountries() {

    this.commonService._getCountries()
      .subscribe(data => {
        this.lstCountries = data;
      },
      error => {
        HandleError.handle(error);
      });
  }

  private round(value, precision) {
    try {
      let multiplier = Math.pow(10, precision || 0);
      return Math.round(value * multiplier) / multiplier;
    }
    catch (e) {
      HandleError.handle(e);
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
