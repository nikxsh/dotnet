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
import { Invoice, InvoiceProduct } from '../../models/invoice.model';
import { Tenant } from '../../Models/profile.model';
//Services
import { CommonService } from '../../services/common.service';
import { TenantService } from '../../services/tenant.service';
import { ProductService } from '../../services/product.service';
import { LocalStorageService } from '../../services/storage.service';
import { InvoiceService } from '../../services/invoice.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime, getFormattedDate } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {

  @ViewChild("InvoiceDetailsForm")
  invoiceDetailsForm: FormControl

  @ViewChild("InvoiceItemsForm")
  invoiceItemsForm: FormControl

  private billingAddressModalRef: BsModalRef;
  private shippingAddressModalRef: BsModalRef;

  private customerDataSource: any;
  private customerTypeaheadNoResults: boolean;
  private customerTypeaheadLoading: boolean;

  private title: string = 'Create Invoice';
  private lstStates: Array<ValueObjectPair> = [];
  private lstCountries: Array<ValueObjectPair> = [];
  private lstCusomerHeads: Array<Catalogue> = [];
  private lstProductHeads: Array<Product> = [];
  private billingAddressModel: Address = new Address();
  private shippingAddressModel: Address = new Address();
  private showIGST: boolean = false;
  private isCustomerSelected: boolean = false;
  private invoiceModel: Invoice;
  private apiMessage: string;
  private invoiceId: string = undefined;
  private saleId: string = undefined;
  private isNewInvoice: boolean = true;
  private message: MessageHandler = new MessageHandler();
  private productTypes: any[] = Global.Product_Types;

  constructor(
    private commonService: CommonService,
    private tenantService: TenantService,
    private productService: ProductService,
    private invoiceService: InvoiceService,
    private storageService: LocalStorageService,
    private router: Router,
    private route: ActivatedRoute,
    private modalServiceRef: BsModalService) {

    this.customerDataSource = Observable
      .create((observer: any) => {
        // Runs on every search
        observer.next(this.invoiceModel.customerName);
      })
      .mergeMap((token: string) => this.getCustomerInfo(token));
      
  }

  ngOnInit(): void {

    this.route.params.forEach((params: Params) => {
      this.invoiceId = params['id'];
      this.saleId = params['saleId'];

      this.invoiceModel = new Invoice();
      this.invoiceModel.salesId = this.saleId;
      this.invoiceModel.tenantInfo = new Tenant();
      this.invoiceModel.customerInfo = new Catalogue(new Address(), new Address());
      this.invoiceModel.products.push(new InvoiceProduct());

      try {
        let tenantInfo = JSON.parse(this.storageService._getTenantInfo()) as Tenant;
        Object.assign(this.invoiceModel.tenantInfo, tenantInfo);

        this.getStates();
        this.getCountries();

        if (this.invoiceId != undefined && this.saleId != undefined) {
          this.isNewInvoice = false;
          this.invoiceModel.salesId = this.saleId;
          this.getInvoiceById(this.invoiceId, true);
        }
        else if (this.invoiceId != undefined) {
          this.isNewInvoice = false;
          this.getInvoiceById(this.invoiceId);
        } else if (this.saleId != undefined) {
          this.isNewInvoice = true;
          this.invoiceModel.invoiceDate = getFormattedDate(new Date().toDateString());
          this.invoiceModel.salesId = this.saleId;
          this.getSalesInvoiceById(this.saleId);
        }
        else {
          this.invoiceModel.invoiceDate = getFormattedDate(new Date().toDateString());
          this.generateInvoiceId();
        }
      }
      catch (e) {
        HandleError.handle(e);
      }
    });
  }

  private addItem(flag: boolean) {
    try {
      if (flag)
        if (this.invoiceItemsForm.valid || this.invoiceModel.products.length == 0)
          this.invoiceModel.products.push(new InvoiceProduct());
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private onQuantityChange(index) {
    try {
      let item = this.invoiceModel.products[index];
      if (this.invoiceModel.salesId != undefined && this.invoiceModel.salesId != null && this.invoiceModel.salesId != '' && (item.quantity > (item.ordered - item.invoiced))) {
        item.quantity = 0;
        this.message.text = 'Quantity should be less than (Quantity - Invoiced)';
        this.message.type = 2;
      }
      else {
        this.onRateChange(index);
        this.addCgst(index);
        this.addSgst(index);
        this.addIgst(index);
      }
    } catch (e) {
      HandleError.handle(e);
    }
  }

  private onRateChange(index) {
    try {
      let product = this.invoiceModel.products[index];
      let amount = this.round(product.quantity * product.rate, 2);
      product.productTotal = amount;
      product.taxable = amount;
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addDiscount(index) {
    try {
      let product = this.invoiceModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.productTotal = this.round(total - ((product.discount / 100) * total), 2);
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addCgst(index) {
    try {
      let product = this.invoiceModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.cgstAmount = this.round((product.cgstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount, 2);
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addSgst(index) {
    try {
      let product = this.invoiceModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.sgstAmount = this.round((product.sgstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount, 2);
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addIgst(index) {
    try {
      let product = this.invoiceModel.products[index];
      let total = this.round(product.quantity * product.rate, 2);
      product.igstAmount = this.round((product.igstSlab / 100) * total, 2);
      product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount + product.igstAmount, 2);
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getTotal() {

    let total = 0;
    try {
      this.invoiceModel.products.forEach(x => total += x.productTotal);
    }
    catch (e) {
      HandleError.handle(e);
    }
    return Math.round(total);
  }

  private getCustomerInfo(token): Observable<any> {
    let query = new RegExp(token, 'ig');
    try {
      var request = new PagingRequest(0, 100);
      request.isEnable = true;
      request.filter.push(new Filter("companyname", token));

      this.tenantService._getCustomerContacts(request)
        .then(result => {
          if (result.status == 1) {
            this.lstCusomerHeads = result.data;
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

    return Observable.of(this.lstCusomerHeads.filter((customer: Catalogue) => {
      return query.test(customer.companyName);
    }));
  }

  public changeCustomerTypeaheadLoading(e: boolean): void {
    this.customerTypeaheadLoading = e;
  }

  public changeCustomerTypeaheadNoResults(e: boolean): void {
    this.customerTypeaheadNoResults = e;
  }

  public typeaheadOnSelect(e: TypeaheadMatch): void {    
    try {
      this.isCustomerSelected = true;
      var info = (e.item as Catalogue);
      Object.assign(this.invoiceModel.customerInfo, info);
      Object.assign(this.billingAddressModel, this.invoiceModel.customerInfo.billingAddress);
      Object.assign(this.shippingAddressModel, this.invoiceModel.customerInfo.shippingAddress);
      this.invoiceModel.customerInfo.gstin = info.gstin;
      this.manipulateTaxWindow();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private manipulateTaxWindow() {
    try {
      var tenantStateCode = this.invoiceModel.tenantInfo.orgProfile.gstin.substr(0, 2).toUpperCase();
      var contactSateCode = this.invoiceModel.customerInfo.gstin.substr(0, 2).toUpperCase();

      if (tenantStateCode === contactSateCode)
        this.showIGST = false;
      else
        this.showIGST = true;
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private productResultFormatter = (result: Product) => result.productName + " (" + result.description + ")";
  private productInputFormatter = (result: Product) => result.productName;

  private searchProducts = (text$: Observable<string>) =>
    map.call(distinctUntilChanged.call(debounceTime.call(text$, 50)),
      term => term.length < 2 ? [] : this.getProductInfo(term).slice(0, 10));

  private getProductInfo(input) {
    try {
      var request = new ProductPagingRequest(this.invoiceModel.products.filter(n => n.id != null || n.id != undefined).map(x => x.id));
      request.filter.push(new Filter("name", input));
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
    return this.lstProductHeads.filter(x => !this.invoiceModel.products.some(p => p.id == x.id));
  }

  private onProductSelection(selectedProduct, index) {
    try {
      let product = this.invoiceModel.products[index];
      product.id = selectedProduct.item.id;
      product.productName = selectedProduct.item.productName;
      product.productType = selectedProduct.item.productType;
      product.productCode = selectedProduct.item.productCode;
      product.uom = selectedProduct.item.uom;
      product.skuCode = selectedProduct.item.skuCode;
      if (this.showIGST) {
        product.igstSlab = selectedProduct.item.taxSlab;
      }
      else {
        product.cgstSlab = selectedProduct.item.taxSlab / 2;
        product.sgstSlab = selectedProduct.item.taxSlab / 2;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private generateInvoiceId() {
    this.invoiceService._getInvoiceNumber()
      .then(result => {
        if (result.status == 1) {
          this.invoiceModel.invoiceNumber = result.data.newInvoiceNumber;
          this.invoiceModel.invoiceSeries = result.data.newInvoiceSeries;
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

  private getInvoiceById(invoiceId, isSalesInvoice = false) {
    try {
      this.invoiceService._getInvoiceById(invoiceId)
        .then(result => {
          if (result.status == 1) {

            this.invoiceModel.id = result.data.id;
            this.invoiceModel.invoiceNumber = result.data.invoiceNumber;
            this.invoiceModel.invoiceDate = result.data.invoiceDate;
            this.invoiceModel.invoiceDate = getFormattedDate(this.invoiceModel.invoiceDate);
            this.invoiceModel.salesId = result.data.salesId;
            this.invoiceModel.referenceNumber = result.data.referenceNumber;
            this.invoiceModel.products = result.data.lineItems;
            this.invoiceModel.payments = result.data.payments;

            this.invoiceModel.customerInfo = new Catalogue(new Address(), new Address());
            this.invoiceModel.customerInfo.gstin = result.data.customerGSTIN;
            this.invoiceModel.customerInfo.companyName = result.data.customerName;
            this.invoiceModel.customerInfo.billingAddress = result.data.billingAddress;
            this.invoiceModel.customerInfo.shippingAddress = result.data.shippingAddress;

            Object.assign(this.billingAddressModel, this.invoiceModel.customerInfo.billingAddress);
            Object.assign(this.shippingAddressModel, this.invoiceModel.customerInfo.shippingAddress);

            this.invoiceModel.totalCGST = result.data.totalCGST;
            this.invoiceModel.totalSGST = result.data.totalSGST;
            this.invoiceModel.totalIGST = result.data.totalIGST;
            this.invoiceModel.totalAmount = result.data.totalAmount;

            if (!isSalesInvoice) {
              this.invoiceModel.products.forEach(x => { x.ordered = x.quantity; x.quantity = x.ordered - x.invoiced; });
            }
            else {
              this.invoiceService._getInvoiceItemsQuantity(this.invoiceModel.salesId)
                .then(result => {
                  if (result.status == 1) {
                    this.invoiceModel.products.forEach(x => {
                      let item = result.data.find(i => i.id == x.id);
                      x.ordered = item.orderedQuantity;
                      x.invoiced = item.invoicedQuantity - x.quantity;
                      //x.quantity = 0;
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

            if (this.invoiceModel.customerInfo.companyName != undefined || this.invoiceModel.customerInfo.companyName != "")
              this.isCustomerSelected = true;

            this.manipulateTaxWindow();
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

  private getSalesInvoiceById(salesId) {
    try {
      this.invoiceService._getSalesInvoiceById(salesId)
        .then(result => {
          if (result.status == 1) {
            this.generateInvoiceId();
            this.invoiceModel.referenceNumber = result.data.salesOrderNumber;
            this.invoiceModel.invoiceDate = getFormattedDate(result.data.salesOrderDate);
            this.invoiceModel.products = result.data.lineItems;
            this.invoiceModel.payments = result.data.payments;
            this.invoiceModel.customerInfo = new Catalogue(new Address(), new Address());
            this.invoiceModel.customerInfo.gstin = result.data.customerGSTIN;
            this.invoiceModel.customerInfo.companyName = result.data.customerName;
            this.invoiceModel.customerInfo.billingAddress = result.data.billingAddress;
            this.invoiceModel.customerInfo.shippingAddress = result.data.shippingAddress;
            getFormattedDate(new Date().toDateString())

            Object.assign(this.billingAddressModel, this.invoiceModel.customerInfo.billingAddress);
            Object.assign(this.shippingAddressModel, this.invoiceModel.customerInfo.shippingAddress);

            this.invoiceModel.totalCGST = result.data.totalCGST;
            this.invoiceModel.totalSGST = result.data.totalSGST;
            this.invoiceModel.totalIGST = result.data.totalIGST;
            this.invoiceModel.totalAmount = result.data.totalAmount;

            this.invoiceModel.products.forEach(x => {
              x.ordered = x.quantity;
              x.quantity = x.ordered - x.invoiced;
              let amount = this.round(x.quantity * x.rate, 2);
              x.taxable = amount;
              if (this.showIGST) {
                x.igstAmount = this.round((x.igstSlab / 100) * amount, 2);
                x.productTotal = amount + x.igstAmount;
              }
              else {
                x.cgstAmount = this.round((x.cgstSlab / 100) * amount, 2);
                x.sgstAmount = this.round((x.sgstSlab / 100) * amount, 2);
                x.productTotal = amount + x.cgstAmount + x.sgstAmount;
              }
            });
            this.invoiceModel.totalAmount = this.getTotal();

            this.manipulateTaxWindow();

            if (this.invoiceModel.customerInfo.companyName != undefined || this.invoiceModel.customerInfo.companyName != "")
              this.isCustomerSelected = true;
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
      this.invoiceModel.products.splice(index, 1);
      this.invoiceModel.totalAmount = this.getTotal();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private updateBillingAddress() {
    try {
      Object.assign(this.invoiceModel.customerInfo.billingAddress, this.billingAddressModel);
    }
    catch (e) {
      HandleError.handle(e);
    }
    this.billingAddressModalRef.hide();
  }

  private updateShippingAddress() {
    try {
      Object.assign(this.invoiceModel.customerInfo.shippingAddress, this.shippingAddressModel);
    }
    catch (e) {
      HandleError.handle(e);
    }
    this.shippingAddressModalRef.hide();
  }

  private saveInvoice() {
    try {
      var totalCGST: number = 0, totalSGST: number = 0, totalIGST: number = 0;
      this.invoiceModel.products.forEach(x => { totalCGST += x.cgstAmount, totalSGST += x.sgstAmount, totalIGST += x.igstAmount });
      this.invoiceModel.totalCGST = totalCGST;
      this.invoiceModel.totalSGST = totalSGST;
      this.invoiceModel.totalIGST = totalIGST;

      this.message.text = "Saving in progress...";
      this.message.type = 1;

      if (this.isNewInvoice && this.saleId == undefined) {
        this.invoiceService._createInvoice(this.invoiceModel)
          .then(result => {
            if (result.status == 1) {
              this.message.text = '';
              this.router.navigate(['app/main/invoices']);
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
      else if (this.isNewInvoice && this.saleId != undefined) {
        this.invoiceService._createSalesInvoice(this.invoiceModel)
          .then(result => {
            if (result.status == 1) {
              this.message.text = '';
              this.router.navigate(['app/main/invoices']);
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
      else if (!this.isNewInvoice && this.saleId != undefined && this.invoiceId != undefined) {
        this.invoiceService._editSalesInvoice(this.invoiceModel)
          .then(result => {
            if (result.status == 1) {
              this.message.text = '';
              this.router.navigate(['app/main/invoices']);
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
      else if (!this.isNewInvoice && this.saleId == undefined && this.invoiceId != undefined) {
        this.invoiceService._editInvoice(this.invoiceModel)
          .then(result => {
            if (result.status == 1) {
              this.message.text = '';
              this.router.navigate(['app/main/invoices']);
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
    try {
      this.commonService._getCountries()
        .subscribe(data => {
          this.lstCountries = data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
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

  public openModal(template: TemplateRef<any>, CASE: number) {
    try {
      switch (CASE) {
        case 1:
          this.billingAddressModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
          break;
        case 2:
          this.shippingAddressModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
          break;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
