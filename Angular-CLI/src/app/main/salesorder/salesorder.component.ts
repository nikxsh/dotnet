//Core
import { Component, OnInit, ElementRef, ViewChild, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { distinctUntilChanged } from 'rxjs/operator/distinctUntilChanged';
import { debounceTime } from 'rxjs/operator/debounceTime';
import { map } from 'rxjs/operator/map';
//Models
import { Payment, MessageHandler, ValueObjectPair, Address, PagingRequest, Filter, ProductPagingRequest } from '../../Models/common.model';
import { Catalogue } from '../../Models/contact.model';
import { Product } from '../../models/product.model';
import { SalesOrder, SalesOrderProduct } from '../../models/salesorder.model';
import { Tenant } from '../../Models/profile.model';
//Service
import { InvoiceService } from '../../services/invoice.service';
import { LocalStorageService } from '../../services/storage.service';
import { ProductService } from '../../services/product.service';
import { TenantService } from '../../services/tenant.service';
import { CommonService } from '../../services/common.service';
import { SalesOrderService } from '../../services/salesorder.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime, getFormattedDate } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-salesorder',
  templateUrl: './salesorder.component.html',
  styleUrls: ['./salesorder.component.css']
})
export class SalesOrderComponent implements OnInit {

  @ViewChild("SalesOrderDetailsForm")
  salesOrderDetailsForm: FormControl

  @ViewChild("SalesOrderItemsForm")
  salesOrderItemsForm: FormControl


  private billingAddressModalRef: BsModalRef;
  private shippingAddressModalRef: BsModalRef;
  private title: string = 'Generare Sales Order';
  private lstStates: Array<ValueObjectPair> = [];
  private lstCountries: Array<ValueObjectPair> = [];
  private lstCusomerHeads: Array<Catalogue> = [];
  private lstProductHeads: Array<Product> = [];
  private billingAddressModel: Address = new Address();
  private shippingAddressModel: Address = new Address();
  private showIGST: boolean = false;
  private isCustomerSelected: boolean = false;
  private salesOrderModel: SalesOrder;
  private apiMessage: string;
  private salesOrderId: string = '';
  private isNewSalesOrder: boolean = true;
  private message: MessageHandler = new MessageHandler();
  private productTypes: any[] = Global.Product_Types;

  constructor(
      private commonService: CommonService,
      private tenantService: TenantService,
      private productService: ProductService,
      private salesOrderService: SalesOrderService,
      private storageService: LocalStorageService,
      private router: Router,
      private route: ActivatedRoute,
      private modalServiceRef: BsModalService) {
  }

  ngOnInit(): void {

      this.route.params.forEach((params: Params) => {
          this.salesOrderId = params['id'];

          this.salesOrderModel = new SalesOrder();
          this.salesOrderModel.salesOrderDate = getFormattedDate(new Date().toDateString());
          this.salesOrderModel.tenantInfo = new Tenant();
          this.salesOrderModel.customerInfo = new Catalogue(new Address(), new Address());
          this.salesOrderModel.products.push(new SalesOrderProduct());

          try {
              let tenantInfo = JSON.parse(this.storageService._getTenantInfo()) as Tenant;
              Object.assign(this.salesOrderModel.tenantInfo, tenantInfo);

              this.getStates();
              this.getCountries();
              if (this.salesOrderId == undefined || this.salesOrderId == "") {
                  this.generateSalesOrderId();
              }
              else {
                  this.isNewSalesOrder = false;
                  this.getSalesOrderById(this.salesOrderId);
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
              if (this.salesOrderItemsForm.valid || this.salesOrderModel.products.length == 0)
                  this.salesOrderModel.products.push(new SalesOrderProduct());
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private onRateChange(index) {
      try {
          let product = this.salesOrderModel.products[index];
          let amount = this.round(product.quantity * product.rate, 2);
          product.productTotal = amount;
          product.taxable = amount;
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private addDiscount(index) {
      try {
          let product = this.salesOrderModel.products[index];
          let total = this.round(product.quantity * product.rate, 2);
          product.productTotal = this.round(total - ((product.discount / 100) * total), 2);
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private addCgst(index) {
      try {
          let product = this.salesOrderModel.products[index];
          let total = this.round(product.quantity * product.rate, 2);
          product.cgstAmount = this.round((product.cgstSlab / 100) * total, 2);
          product.productTotal = this.round(total + product.cgstAmount, 2);
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private addSgst(index) {
      try {
          let product = this.salesOrderModel.products[index];
          let total = this.round(product.quantity * product.rate, 2);
          product.sgstAmount = this.round((product.sgstSlab / 100) * total, 2);
          product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount, 2);
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private addIgst(index) {
      try {
          let product = this.salesOrderModel.products[index];
          let total = this.round(product.quantity * product.rate, 2);
          product.igstAmount = this.round((product.igstSlab / 100) * total, 2);
          product.productTotal = this.round(total + product.cgstAmount + product.sgstAmount + product.igstAmount, 2);
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private getTotal() {

      let total = 0;
      try {
          this.salesOrderModel.products.forEach(x => total += x.productTotal);
      }
      catch (e) {
        HandleError.handle(e);
      }
      return Math.round(total);
  }

  private customerResultFormatter = (result: Catalogue) => result.companyName;
  private customerInputFormatter = (result: Catalogue) => result.companyName;

  private searchCustomers = (text$: Observable<string>) =>
      map.call(distinctUntilChanged.call(debounceTime.call(text$, 50)),
          term => term.length < 2 ? [] : this.getCustomerInfo(term).slice(0, 10));

  private getCustomerInfo(input) {

      try {
          var request = new PagingRequest(0, 100);
          request.isEnable = true;
          request.filter.push(new Filter("companyname", input));

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
      return this.lstCusomerHeads;
  }

  private onCustomerSelection(data) {
      try {
          this.isCustomerSelected = true;
          var info = (data.item as Catalogue);
          Object.assign(this.salesOrderModel.customerInfo, info);
          Object.assign(this.billingAddressModel, this.salesOrderModel.customerInfo.billingAddress);
          Object.assign(this.shippingAddressModel, this.salesOrderModel.customerInfo.shippingAddress);
          this.salesOrderModel.customerInfo.gstin = info.gstin;
          this.manipulateTaxWindow();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private manipulateTaxWindow() {
      try {
          var tenantStateCode = this.salesOrderModel.tenantInfo.orgProfile.gstin.substr(0, 2).toUpperCase();
          var contactSateCode = this.salesOrderModel.customerInfo.gstin.substr(0, 2).toUpperCase();

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

          var request = new ProductPagingRequest(this.salesOrderModel.products.filter(n => n.id != null || n.id != undefined).map(x => x.id));   
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
      return this.lstProductHeads.filter(x => !this.salesOrderModel.products.some(p => p.id == x.id));
  }

  private onProductSelection(selectedProduct, index) {
      try {
          let product = this.salesOrderModel.products[index];
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

  private generateSalesOrderId() {
      try {
          this.salesOrderService._getSalesOrderNumber()
              .then(result => {
                  if (result.status == 1) {
                      this.salesOrderModel.salesOrderNumber = result.data.newSalesOrderNumber;
                      this.salesOrderModel.salesOrderSeries = result.data.newSalesOrderSeries;
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

  private getSalesOrderById(salesOrderId) {
      try {
          this.salesOrderService._getSalesOrderById(salesOrderId)
              .then(result => {
                  if (result.status == 1) {
                      this.salesOrderModel.id = result.data.id;
                      this.salesOrderModel.salesOrderNumber = result.data.salesOrderNumber;
                      this.salesOrderModel.salesOrderDate = result.data.salesOrderDate;
                      this.salesOrderModel.salesOrderStatus = result.data.salesOrderStatus;
                      this.salesOrderModel.salesOrderDate = getFormattedDate(this.salesOrderModel.salesOrderDate);
                      this.salesOrderModel.products = result.data.lineItems;

                      this.salesOrderModel.customerInfo = new Catalogue(new Address(), new Address());
                      this.salesOrderModel.customerInfo.gstin = result.data.customerGSTIN;
                      this.salesOrderModel.customerInfo.companyName = result.data.customerName;
                      this.salesOrderModel.customerInfo.billingAddress = result.data.billingAddress;
                      this.salesOrderModel.customerInfo.shippingAddress = result.data.shippingAddress;

                      Object.assign(this.billingAddressModel, this.salesOrderModel.customerInfo.billingAddress);
                      Object.assign(this.shippingAddressModel, this.salesOrderModel.customerInfo.shippingAddress);

                      this.salesOrderModel.totalCGST = result.data.totalCGST;
                      this.salesOrderModel.totalSGST = result.data.totalSGST;
                      this.salesOrderModel.totalIGST = result.data.totalIGST;
                      this.salesOrderModel.totalAmount = result.data.totalAmount;

                      this.manipulateTaxWindow();

                      if (this.salesOrderModel.customerInfo.companyName != undefined || this.salesOrderModel.customerInfo.companyName != "")
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
          this.salesOrderModel.products.splice(index, 1);
          this.salesOrderModel.totalAmount = this.getTotal();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private updateBillingAddress() {
      try {
          Object.assign(this.salesOrderModel.customerInfo.billingAddress, this.billingAddressModel);
      }
      catch (e) {
        HandleError.handle(e);
      }
      finally{
        this.billingAddressModalRef.hide();
      }
  }

  private updateShippingAddress() {
      try {
          Object.assign(this.salesOrderModel.customerInfo.shippingAddress, this.shippingAddressModel);
      }
      catch (e) {
        HandleError.handle(e);
      }
      finally{
        this.shippingAddressModalRef.hide();
      }
  }

  private saveSalesOrder() {
      try {
          var totalCGST: number = 0, totalSGST: number = 0, totalIGST: number = 0;
          this.salesOrderModel.products.forEach(x => { totalCGST += x.cgstAmount, totalSGST += x.sgstAmount, totalIGST += x.igstAmount });
          this.salesOrderModel.totalCGST = totalCGST;
          this.salesOrderModel.totalSGST = totalSGST;
          this.salesOrderModel.totalIGST = totalIGST;

          this.message.text = "Saving in progress...";
          this.message.type = 1;

          if (this.isNewSalesOrder) {
              this.salesOrderService._generateSalesOrder(this.salesOrderModel)
                  .then(result => {
                      if (result.status == 1) {
                          this.message.text = '';
                          this.router.navigate(['app/main/salesorders']);
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
              this.salesOrderService._editSalesOrder(this.salesOrderModel)
                  .then(result => {
                      if (result.status == 1) {
                          this.message.text = '';
                          this.router.navigate(['app/main/salesorders']);
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
                  this.message.text = Global.UI_ERROR;
                  this.message.type = 2;
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
