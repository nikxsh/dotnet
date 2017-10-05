//core
import { Component, OnInit, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { ValueObjectPair, Address, MessageHandler, PagingRequest, Filter, ProductPagingRequest } from '../../Models/common.model';
import { Catalogue } from '../../Models/contact.model';
import { Product } from '../../models/product.model';
import { PurchaseOrder, PurchaseOrderProduct, BasePurchaseOrder, PurchaseReceive, PurchaseReceiveItem, PurchaseOrderStatus } from '../../models/purchaseorder.model';
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
import { Router } from '@angular/router';

@Component({
  selector: 'app-purchaseordertable',
  templateUrl: './purchaseordertable.component.html',
  styleUrls: ['./purchaseordertable.component.css']
})
export class PurchaseOrderTableComponent implements OnInit {

  @ViewChild('PurchaseOrderContainer')
  PurchaseOrderContainer: ElementRef;

  @ViewChild("PurchaseOrderItemsForm")
  purchaseOrderItemsForm: FormControl

  private removePurchaseOrderModalRef: BsModalRef;
  private addViewPurchaseOrderModalRef: BsModalRef;
  private lstPurchaseOrders: BasePurchaseOrder[] = [];
  private tenantInfo: Tenant;
  private title: string = "Purchase Orders";
  private purchaseOrderModel: PurchaseOrder;
  private selectedPurchaseOrderIndex: number = -1;
  private tenantLogoURL: string;
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();
  private showBillMenu: boolean = true;
  private purchaseOrderReceiveModel: PurchaseReceive;
  private readOnlyRow: boolean = false;
  private lstPurchaseReceiveItem: PurchaseReceiveItem[] = [];

  constructor(
    private purchaseOrderService: PurchaseOrderService,
    private router: Router,
    private storageService: LocalStorageService,
    private modalServiceRef: BsModalService) {
  }

  ngOnInit() {
    try {
      this.purchaseOrderModel = new PurchaseOrder();
      this.purchaseOrderReceiveModel = new PurchaseReceive();
      this.purchaseOrderReceiveModel.receivedOnDate = getFormattedDate(new Date().toDateString());
      this.purchaseOrderReceiveModel.receiveLineItem = [];
      this.getAllPurchaseOrders();
    }
    catch (e) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }

  }

  private getAllPurchaseOrders() {
    this.message.text = '';
    try {
      this.message.text = '';
      this.purchaseOrderService._getAllPurchaseOrders()
        .then(result => {
          if (result.status == 1) {

            this.lstPurchaseOrders = result.data;

            if (this.lstPurchaseOrders.length != 0)
              this.getPurchaseOrderById(this.lstPurchaseOrders[0].id);
            else {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Purchase Orders');
              this.message.type = 3;
            }
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

  private getPurchaseOrderById(purchaseOrderId, index = 0) {
    this.message.text = '';
    try {
      this.selectedPurchaseOrderIndex = index;
      this.purchaseOrderService._getPurchaseOrderById(purchaseOrderId)
        .then(result => {
          if (result.status == 1) {
            this.purchaseOrderModel.id = result.data.id;
            this.purchaseOrderModel.purchaseOrderDate = result.data.purchaseOrderDate;
            this.purchaseOrderModel.purchaseOrderNumber = result.data.purchaseOrderNumber;
            this.purchaseOrderModel.status = result.data.purchaseOrderStatus;
            this.purchaseOrderModel.products = result.data.lineItems;
            this.purchaseOrderModel.purchaseReceive = result.data.purchaseReceive;
            this.purchaseOrderModel.totalAmount = result.data.totalAmount;
            this.purchaseOrderModel.purchaseOrderHtml = result.data.purchaseOrderHtml;
            this.PurchaseOrderContainer.nativeElement.innerHTML = this.purchaseOrderModel.purchaseOrderHtml;
            this.calculateRecievedLineItem();
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

  private createPurchaseOrder() {
    this.message.text = '';
    this.router.navigate(['app/main/addpurchaseorder']);
  }

  private removePurchaseOrder(purchaseOrderId) {
    this.message.text = '';
    this.purchaseOrderModel.id = purchaseOrderId;
  }

  private deletePurchaseOrder() {
    this.message.text = '';
    try {
      this.purchaseOrderService._deletePurchaseOrder(this.purchaseOrderModel.id)
        .then(result => {
          if (result.status == 1) {

            this.lstPurchaseOrders.splice(this.lstPurchaseOrders.findIndex(x => x.id == result.data), 1);

            if (this.lstPurchaseOrders.length > 0)
              this.getPurchaseOrderById(this.lstPurchaseOrders[0].id);

            this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Purchase Order');
            this.message.type = 1;
            this.message.isGlobal = true;
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

  private print() {
    var printContent = this.purchaseOrderModel.purchaseOrderHtml.replace(`class="Custom-tbody-sidebar"`, "");
    let printWindow = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    printWindow.document.write(`<html>
                                      <head>
                                          <title>${this.purchaseOrderModel.purchaseOrderNumber}</title>
                                          <link href= "Content/css/bootstrap.css" rel= "stylesheet" />
                                          <link href="Content/css/Site.css" rel= "stylesheet" />    
                                      </head>
                                      <body onload="window.print()">
                                          ${printContent}
                                      </body>
                                  </html>`);
    printWindow.document.close();
  }

  private viewPurchaseReceive(receiveNumber) {
    try {
      this.message.text = '';
      this.readOnlyRow = true;
      Object.assign(this.purchaseOrderReceiveModel, this.purchaseOrderModel.purchaseReceive.find(x => x.receiveNumber == receiveNumber));
      this.purchaseOrderReceiveModel.receiveLineItem.forEach(x => {
        let product = this.purchaseOrderModel.products.find(y => y.id == x.productId);
        x.productName = product.productName;
        x.quantity = product.quantity;
        x.received = x.quantityToReceive;
      });
      this.purchaseOrderReceiveModel.receivedOnDate = getFormattedDate(this.purchaseOrderReceiveModel.receivedOnDate);
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private onAddPurchaseReceiveClick() {
    try {
      this.calculateRecievedLineItem();
      this.message.text = '';
      this.purchaseOrderReceiveModel.receiveNumber = '';
      this.purchaseOrderReceiveModel.receivedOnDate = getFormattedDate(new Date().toDateString());
      this.readOnlyRow = false;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private onQuantityChange(index) {
    try {
      this.message.isGlobal = false;
      let item = this.purchaseOrderReceiveModel.receiveLineItem[index];
      if (item.quantityToReceive > (item.quantity - item.received)) {
        this.message.text = 'Quantity must not exceed than desired target';
        this.message.type = 2;
      }
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private addPurchaseReceive() {
    try {
      this.message.text = '';
      this.message.isGlobal = false;
      this.progressing = true;

      var saveModel = new PurchaseReceive();
      saveModel.receivedOnDate = this.purchaseOrderReceiveModel.receivedOnDate;
      saveModel.receiveNumber = this.purchaseOrderReceiveModel.receiveNumber;
      saveModel.receiveLineItem = this.purchaseOrderReceiveModel.receiveLineItem.map(x => new PurchaseReceiveItem(x.productId, x.productName, x.quantity, x.received, x.quantityToReceive));

      this.purchaseOrderService._addPurchaseReceive(this.purchaseOrderModel.id, saveModel)
        .then(result => {
          if (result.status == 1) {
            this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Purchase receive');
            this.message.type = 1;
            this.message.isGlobal = true;
            this.addViewPurchaseOrderModalRef.hide();
            this.purchaseOrderModel.purchaseReceive.unshift(saveModel);
            this.updatePurchaseReceiveStatus(this.purchaseOrderModel.id);
            this.calculateRecievedLineItem(1, saveModel.receiveLineItem);
            this.purchaseOrderReceiveModel.receivedOnDate = getFormattedDate(new Date().toDateString());
          }
          else {
            this.message.text = result.message;
            this.message.type = 2;
          }
          this.progressing = false;
        },
        error => {
          this.progressing = false;
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
        });
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private removePurchaseReceive(receiveNumber) {
    try {
      this.message.text = '';
      this.progressing = true;
      var deleteModel = Object.assign({}, this.purchaseOrderModel.purchaseReceive.find(pr => pr.receiveNumber == receiveNumber));

      this.purchaseOrderService._removePurchaseReceive(this.purchaseOrderModel.id, deleteModel)
        .then(result => {
          if (result.status == 1) {
            this.purchaseOrderModel.purchaseReceive.splice(this.purchaseOrderModel.purchaseReceive.findIndex(x => x.receiveNumber == deleteModel.receiveNumber), 1);

            this.calculateRecievedLineItem(2, deleteModel.receiveLineItem);
            this.updatePurchaseReceiveStatus(this.purchaseOrderModel.id);
            this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Purchase receive');
            this.message.type = 1;
            this.message.isGlobal = true;
          }
          else {
            this.message.text = result.message;
            this.message.type = 2;
          }
          this.progressing = false;
        },
        error => {
          this.progressing = false;
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
        });
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private calculateRecievedLineItem(operation: number = 0, updatedReceiveLineItem: PurchaseReceiveItem[] = []) {
    try {
      if (operation == 1)
        updatedReceiveLineItem.forEach(r => {
          this.purchaseOrderModel.products.find(p => p.id == r.productId).received += r.quantityToReceive;
        });
      else if (operation == 2)
        updatedReceiveLineItem.forEach(r => {
          this.purchaseOrderModel.products.find(p => p.id == r.productId).received -= r.quantityToReceive;
        });

      this.purchaseOrderReceiveModel.receiveLineItem = this.purchaseOrderModel.products.map(x => new PurchaseReceiveItem(x.id, x.productName, x.quantity, x.received, x.quantity - x.received));
    }
    catch (e) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private updatePurchaseReceiveStatus(purchaseId) {
    try {
      let invoice = this.lstPurchaseOrders.find(x => x.id == purchaseId);
      if (this.purchaseOrderModel.purchaseReceive.length == 0)
        invoice.status = PurchaseOrderStatus.Open;
      else
        invoice.status = PurchaseOrderStatus.PartiallyReceived;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private getPrintHtml() {
    return this.purchaseOrderModel.purchaseOrderHtml.replace(`class="Custom-tbody-sidebar"`, "");
  }

  private saveAsPdf() {
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

  public openModal(template: TemplateRef<any>, CASE: number) {
    try {
      switch (CASE) {
        case 1:
          this.addViewPurchaseOrderModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
          break;
        case 2:
          this.removePurchaseOrderModalRef = this.modalServiceRef.show(template, { class: 'modal-sm' });
          break;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
