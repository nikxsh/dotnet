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
import { MessageHandler, Payment } from '../../Models/common.model';
import { Bill, BaseBill } from '../../models/bill.model';
import { Tenant } from '../../Models/profile.model';
import { DeletedLineItems } from '../../models/invoice.model';
//Services
import { CommonService } from '../../services/common.service';
import { TenantService } from '../../services/tenant.service';
import { ProductService } from '../../services/product.service';
import { BillService } from '../../services/bill.service';
import { LocalStorageService } from '../../services/storage.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime, getFormattedDate } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-billtable',
  templateUrl: './billtable.component.html',
  styleUrls: ['./billtable.component.css']
})
export class BillTableComponent implements OnInit {

  @ViewChild('BillContainer') BillContainer: ElementRef;

  private removeBillModalRef: BsModalRef;
  private lstBills: BaseBill[] = [];
  private tenantInfo: Tenant;
  private title: string = "Bills";
  private billModel: Bill;
  private paymentModel: Payment;
  private selectedBillPayments: Payment[] = [];
  private selectedBillIndex: number = -1;
  private tenantLogoURL: string;
  private lstPaymentModes: any[] = [
    { value: 'Cash', key: 0 },
    { value: 'Cheque', key: 1 },
    { value: 'Demand Draft', key: 2 },
    { value: 'Credit/Debit Card', key: 3 }];
  private unpaidAmount: number = 0;
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();
  private showBillMenu: boolean = true;

  constructor(
    private billService: BillService,
    private router: Router,
    private storageService: LocalStorageService,
    private modalServiceRef: BsModalService ) {
    this.tenantInfo = new Tenant();
    this.billModel = new Bill();
    this.paymentModel = new Payment();
  }

  ngOnInit() {
    this.getAllBills();
    this.initValues();
  }

  private initValues() {
    try {
      this.paymentModel.paymentMode = this.lstPaymentModes[0].key;
      Object.assign(this.paymentModel, new Payment());
      this.paymentModel.paymentDate = getFormattedDate(new Date().toDateString());
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getAllBills() {

    try {
      this.message.text = '';
      this.billService._getAllBills()
        .then(result => {
          if (result.status == 1) {

            this.lstBills = result.data;

            if (this.lstBills.length != 0)
              this.getBillById(this.lstBills[0].id);
            else {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Bills');
              this.message.type = 3;
            }
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

  private getBillById(BillId, index = 0) {
    try {
      this.message.text = '';
      this.selectedBillIndex = index;
      this.initValues();
      this.billService._getBillById(BillId)
        .then(result => {
          if (result.status == 1) {
            this.billModel = result.data;
            this.billModel.id = result.data.id;
            this.billModel.billNumber = result.data.billNumber;
            this.billModel.purchaseId = result.data.purchaseId;
            this.billModel.receiveNumber = result.data.recieveNumber;
            this.billModel.referenceNumber = result.data.referenceNumber;
            this.billModel.status = result.data.invoiceStatus;
            this.billModel.totalAmount = result.data.totalAmount;
            this.billModel.vendorName = result.data.customerName;
            this.billModel.products = result.data.lineItems;
            this.BillContainer.nativeElement.innerHTML = result.data.billHtml;
            this.selectedBillPayments = result.data.payments;
            this.setUnpaidAmount();
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

  private payBillAmount() {
    try {
      this.message.text = '';
      if (this.paymentModel.amount <= this.unpaidAmount) {
        var payment = Object.assign({}, this.paymentModel);
        this.updateBillPayments(true, payment);
      }
      else {
        this.message.text = "Amount should be eqaul to or less than unpaid amount.";
        this.message.type = 2;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private updateBillPayments(isAddCommand, model) {
    try {
      this.message.text = '';
      this.managePaymentList(isAddCommand, model);
      if (isAddCommand)
        this.progressing = true;
      this.billService._payBillAmount(model, this.billModel.id, this.billModel.purchaseId)
        .then(result => {
          if (result.status == 1) {
            if (this.billModel.payments.length > 0 && isAddCommand)
              this.billModel.payments[this.billModel.payments.length - 1].paymentId = result.data;

            Object.assign(this.paymentModel, new Payment());
            this.setUnpaidAmount();
            this.updateBillStatus(this.billModel.id);
            this.initValues();
            this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Payment');
            this.message.type = 1;
          }
          else {
            this.message.text = result.message;
            this.message.type = 2;
            this.managePaymentList(!isAddCommand, model);
          }
          this.progressing = false;
        },
        error => {
          this.progressing = false;
          HandleError.handle(error);
          this.managePaymentList(!isAddCommand, model);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private createBill() {
    this.router.navigate(['app/main/addbill']);
  }

  private removeAmount(index) {
    try {
      this.message.text = '';
      let item = this.selectedBillPayments[index];
      this.updateBillPayments(false, item);
      this.message.type = 1;

      if (this.unpaidAmount != this.billModel.totalAmount)
        this.showBillMenu = false;
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private managePaymentList(flag, model: Payment) {
    try {
      if (flag)
        this.selectedBillPayments.push(Object.assign({}, model));
      else
        this.selectedBillPayments.splice(this.selectedBillPayments.indexOf(model), 1);
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private removeBill(billId) {
    this.message.text = '';
    if (this.billModel.id != billId)
      this.getBillById(billId);
    else
      this.billModel.id = billId;

  }

  private deleteBill() {
    try {
      this.message.text = '';
      if (this.billModel.purchaseId == undefined && this.billModel.receiveNumber == undefined) {
        this.billService._deleteBillById(this.billModel.id)
          .then(result => {
            if (result.status == 1) {

              this.lstBills.splice(this.lstBills.findIndex(x => x.id == result.data), 1);

              if (this.lstBills.length > 0)
                this.getBillById(this.lstBills[0].id);

              this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Bill');
              this.message.type = 1;
              this.message.isGlobal = true;
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

        let deletedItems: DeletedLineItems[] = [];
        for (let x of this.billModel.products)
          deletedItems.push(new DeletedLineItems(x.id, x.quantity));

        this.billService._deletePurchaseRecieveBillById(this.billModel.id, this.billModel.purchaseId, deletedItems)
          .then(result => {
            if (result.status == 1) {

              this.lstBills.splice(this.lstBills.findIndex(x => x.id == this.billModel.id), 1);
              if (this.lstBills.length <= 0)
                this.getBillById(this.lstBills[0].id);

              this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Bill');
              this.message.type = 1;
              this.message.isGlobal = true;
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

  private print() {
    try {
      var printContent = this.billModel.billHtml.replace(`class="Custom-tbody-sidebar"`, "");
      let printWindow = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
      printWindow.document.write(`<html>
                                          <head>
                                              <title>${this.billModel.billNumber}</title>
                                              <link href= "Content/css/bootstrap.css" rel= "stylesheet" />
                                              <link href="Content/css/Site.css" rel= "stylesheet" />    
                                          </head>
                                          <body onload="window.print()">
                                              ${printContent}
                                          </body>
                                      </html>`);
      printWindow.document.close();
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getPrintHtml() {
    return this.billModel.billHtml.replace(`class="Custom-tbody-sidebar"`, "");
  }

  private saveAsPdf() {
  }

  private getPaidAmount() {
    try {
      var totalAmount = 0;
      this.selectedBillPayments.forEach(x => { totalAmount = totalAmount + x.amount });
      return totalAmount;
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private setUnpaidAmount() {
    try {
      this.unpaidAmount = this.round(this.billModel.totalAmount - this.getPaidAmount(), 2);
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private updateBillStatus(BillId) {
    try {
      let Bill = this.lstBills.find(x => x.id == BillId);

      if (this.unpaidAmount == this.billModel.totalAmount)
        Bill.status = 0;
      else if (this.unpaidAmount < this.billModel.totalAmount)
        Bill.status = 1;
      else if (this.unpaidAmount == 0)
        Bill.status = 2;
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private ResetBillAmount() {
    try {
      this.paymentModel.amount = 0;
      this.paymentModel.referenceNumber = "";
      this.paymentModel.paymentMode = this.lstPaymentModes[0].key;
      this.paymentModel.paymentDate = getFormattedDate(new Date().toDateString());
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

  public openModal(template: TemplateRef<any>) {
    try {
      this.removeBillModalRef = this.modalServiceRef.show(template, { class: 'modal-sm' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
