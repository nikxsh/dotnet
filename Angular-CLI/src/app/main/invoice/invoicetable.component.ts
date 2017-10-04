//Core
import { Component, OnInit, ElementRef, ViewChild, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { BaseInvoice, Invoice, DeletedLineItems } from '../../models/invoice.model';
import { Tenant } from '../../Models/profile.model';
import { Payment, MessageHandler } from '../../Models/common.model';
//Service
import { InvoiceService } from '../../services/invoice.service';
import { LocalStorageService } from '../../services/storage.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime, getFormattedDate } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-invoicetable',
  templateUrl: './invoicetable.component.html',
  styleUrls: ['./invoicetable.component.css']
})
export class InvoiceTableComponent implements OnInit {
  @ViewChild('InvoiceContainer') InvoiceContainer: ElementRef;

  private removeItemModalRef: BsModalRef;
  private lstInvoices: BaseInvoice[] = [];
  private tenantInfo: Tenant;
  private title: string = "Invoices";
  private invoiceModel: Invoice;
  private paymentModel: Payment;
  private selectedInvoiceIndex: number = -1;
  private tenantLogoURL: string;
  private lstPaymentModes: any[] = [
    { value: 'Cash', key: 0 },
    { value: 'Cheque', key: 1 },
    { value: 'Demand Draft', key: 2 },
    { value: 'Credit/Debit Card', key: 3 }];
  private unpaidAmount: number = 0;
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();

  constructor(
    private invoiceService: InvoiceService,
    private router: Router,
    private storageService: LocalStorageService,
    private modalServiceRef: BsModalService) {
    this.tenantInfo = new Tenant();
    this.invoiceModel = new Invoice();
    this.paymentModel = new Payment();
  }

  ngOnInit() {
    this.getAllInvoices();
    this.initValues();
  }

  private initValues() {
    try {
      this.paymentModel.paymentMode = this.lstPaymentModes[0].key;
      Object.assign(this.paymentModel, new Payment());
      this.paymentModel.paymentDate = getFormattedDate(new Date().toDateString());
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private getAllInvoices() {
    try {
      this.message.text = '';
      this.invoiceService._getAllInvoices()
        .then(result => {
          if (result.status == 1) {

            this.lstInvoices = result.data;

            if (this.lstInvoices.length != 0)
              this.getInvoiceById(this.lstInvoices[0].id);
            else {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Invoices');
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
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private getInvoiceById(invoiceId, index = 0) {
    try {
      this.message.text = '';
      this.selectedInvoiceIndex = index;
      this.initValues();
      this.invoiceService._getInvoiceById(invoiceId)
        .then(result => {
          if (result.status == 1) {
            this.invoiceModel.id = result.data.id;
            this.invoiceModel.salesId = result.data.salesId;
            this.invoiceModel.invoiceNumber = result.data.invoiceNumber;
            this.invoiceModel.products = result.data.lineItems;
            this.invoiceModel.referenceNumber = result.data.referenceNumber;
            this.invoiceModel.status = result.data.invoiceStatus;
            this.invoiceModel.totalAmount = result.data.totalAmount;
            this.invoiceModel.customerName = result.data.customerName;
            this.invoiceModel.invoiceHtml = result.data.invoiceHtml;
            this.InvoiceContainer.nativeElement.innerHTML = this.invoiceModel.invoiceHtml;
            this.invoiceModel.payments = result.data.payments;
            this.setUnpaidAmount();
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
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private payInvoiceAmount() {
    try {
      if (this.paymentModel.amount <= this.unpaidAmount) {
        var payment = Object.assign({}, this.paymentModel);
        this.updateInvoicePayments(true, payment);
      }
      else {
        this.message.text = "Amount should be eqaul to or less than unpaid amount.";
        this.message.type = 2;
      }
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private updateInvoicePayments(isAddCommand, model) {
    try {
      this.message.text = '';
      this.managePaymentList(isAddCommand, model);
      if (isAddCommand)
        this.progressing = true;
      this.invoiceService._payInvoiceAmount(model, this.invoiceModel.id, this.invoiceModel.salesId)
        .then(result => {
          if (result.status == 1) {

            if (this.invoiceModel.payments.length > 0 && isAddCommand)
              this.invoiceModel.payments[this.invoiceModel.payments.length - 1].paymentId = result.data;

            Object.assign(this.paymentModel, new Payment());
            this.setUnpaidAmount();
            this.updateInvoiceStatus(this.invoiceModel.id);
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
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
          this.managePaymentList(!isAddCommand, model);
        });
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private createInvoice() {
    this.router.navigate(['app/main/addinvoice']);
  }

  private removeAmount(index) {
    try {
      let item = this.invoiceModel.payments[index];
      this.updateInvoicePayments(false, item);
      this.message.type = 1;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private managePaymentList(flag, model: Payment) {
    try {
      if (flag)
        this.invoiceModel.payments.push(Object.assign({}, model));
      else
        this.invoiceModel.payments.splice(this.invoiceModel.payments.indexOf(model), 1);
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private removeInvoice(intvoiceId) {
    this.message.text = '';
    if (this.invoiceModel.id != intvoiceId)
      this.getInvoiceById(intvoiceId);
    else
      this.invoiceModel.id = intvoiceId;
  }

  private deleteInvoice() {
    try {
      this.message.text = '';
      if (this.invoiceModel.salesId == undefined || this.invoiceModel.salesId == null) {
        this.invoiceService._deleteInvoiceById(this.invoiceModel.id)
          .then(result => {
            if (result.status == 1) {
              this.lstInvoices.splice(this.lstInvoices.findIndex(x => x.id == this.invoiceModel.id), 1);
              if (this.lstInvoices.length > 0)
                this.getInvoiceById(this.lstInvoices[0].id);

              this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Invoice');
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
      else {
        let deletedItems: DeletedLineItems[] = [];
        for (let x of this.invoiceModel.products)
          deletedItems.push(new DeletedLineItems(x.id, x.quantity));

        this.invoiceService._deleteSalesInvoiceById(this.invoiceModel.id, this.invoiceModel.salesId, deletedItems)
          .then(result => {
            if (result.status == 1) {
              this.lstInvoices.splice(this.lstInvoices.findIndex(x => x.id == this.invoiceModel.id), 1);
              if (this.lstInvoices.length <= 0)
                this.getInvoiceById(this.lstInvoices[0].id);
              this.message.isGlobal = true;
              this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Invoice');
              this.message.type = 1;
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
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private print() {
    try {
      var printContent = this.invoiceModel.invoiceHtml.replace(`class="Custom-tbody-sidebar"`, "");
      let printWindow = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
      printWindow.document.write(`<html>
                                          <head>
                                              <title>${this.invoiceModel.invoiceNumber}</title>
                                              <link href= "Content/css/bootstrap.css" rel= "stylesheet" />
                                              <link href="Content/css/Site.css" rel= "stylesheet" />    
                                          </head>
                                          <body onload="window.print()">
                                              ${printContent}
                                          </body>
                                      </html>`);
      printWindow.document.close();
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private getPrintHtml() {
    return this.invoiceModel.invoiceHtml.replace(`class="Custom-tbody-sidebar"`, "");
  }

  private saveAsPdf() {
  }

  private getPaidAmount() {
    try {
      var totalAmount = 0;
      this.invoiceModel.payments.forEach(x => { totalAmount = totalAmount + x.amount });
      return totalAmount;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private setUnpaidAmount() {
    try {
      this.unpaidAmount = this.round(this.invoiceModel.totalAmount - this.getPaidAmount(), 2);
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private updateInvoiceStatus(invoiceId) {
    try {
      let invoice = this.lstInvoices.find(x => x.id == invoiceId);

      if (this.unpaidAmount == this.invoiceModel.totalAmount)
        invoice.status = 0;
      else if (this.unpaidAmount < this.invoiceModel.totalAmount)
        invoice.status = 1;
      else if (this.unpaidAmount == 0)
        invoice.status = 2;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private ResetInvoiceAmount() {
    try {
      this.paymentModel.amount = 0;
      this.paymentModel.referenceNumber = "";
      this.paymentModel.paymentMode = this.lstPaymentModes[0].key;
      this.paymentModel.paymentDate = getFormattedDate(new Date().toDateString());
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }

  }

  private round(value, precision) {
    try {
      let multiplier = Math.pow(10, precision || 0);
      return Math.round(value * multiplier) / multiplier;
    }
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  public openModal(template: TemplateRef<any>, CASE: number) {
    try {
      this.removeItemModalRef = this.modalServiceRef.show(template, { class: 'modal-sm' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
