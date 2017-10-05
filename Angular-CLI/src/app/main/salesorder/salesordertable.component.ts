//Core
import { Component, OnInit, ElementRef, ViewChild, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
//Models
import { BaseSalesOrder, SalesOrder } from '../../models/salesorder.model';
import { Tenant } from '../../Models/profile.model';
import { MessageHandler } from '../../Models/common.model';
//Service
import { SalesOrderService } from '../../services/salesorder.service';
import { LocalStorageService } from '../../services/storage.service';
//Helper
import * as Global from '../../global'
import { getLogoURL } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-salesordertable',
  templateUrl: './salesordertable.component.html',
  styleUrls: ['./salesordertable.component.css']
})
export class SalesOrderTableComponent implements OnInit {

  @ViewChild('SalesOrderContainer') SalesOrderContainer: ElementRef;

  private removeSalesOrderModalRef: BsModalRef;
  private lstSalesOrders: BaseSalesOrder[] = [];
  private tenantInfo: Tenant;
  private title: string = "Sales Orders";
  private salesOrderModel: SalesOrder;
  private selectedSalesOrderIndex: number = -1;
  private tenantLogoURL: string;
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();
  private showSalesOrderMenu: boolean = true;
  private salesInvoiceStatus = ['Open', 'Partially Paid', 'Paid'];

  constructor(
    private salesOrderService: SalesOrderService,
    private router: Router,
    private storageService: LocalStorageService,
    private modalServiceRef: BsModalService ) {
    this.tenantInfo = new Tenant();
    this.salesOrderModel = new SalesOrder();
  }

  ngOnInit() {
    try {
      this.message.text = '';
      this.getAllSalesOrders();
      let tenantInfo = JSON.parse(this.storageService._getTenantInfo()) as Tenant;
      Object.assign(this.tenantInfo, tenantInfo);
      this.tenantLogoURL = getLogoURL(this.tenantInfo.id);
    }
    catch (e) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private getAllSalesOrders() {
    try {
      this.message.text = '';
      this.salesOrderService._getAllSalesOrders()
        .then(result => {
          if (result.status == 1) {

            this.lstSalesOrders = result.data;

            if (this.lstSalesOrders.length != 0)
              this.getSalesOrderById(this.lstSalesOrders[0].id);
            else {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Sales Orders');
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

  private getSalesOrderById(salesOrderId, index = 0) {
    try {
      this.selectedSalesOrderIndex = index;
      this.salesOrderService._getSalesOrderById(salesOrderId)
        .then(result => {
          if (result.status == 1) {
            this.salesOrderModel.id = result.data.id;
            this.salesOrderModel.salesOrderNumber = result.data.salesOrderNumber;
            this.salesOrderModel.salesOrderStatus = result.data.salesOrderStatus;
            this.salesOrderModel.invoices = result.data.invoices;
            this.salesOrderModel.salesOrderHtml = result.data.salesOrderHtml;
            this.SalesOrderContainer.nativeElement.innerHTML = this.salesOrderModel.salesOrderHtml;
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

  private generateSalesOrder() {
    this.router.navigate(['app/main/generatesalesorder']);
  }

  private removeSalesOrder(salesOrderId) {
    this.message.text = '';
    this.salesOrderModel.id = salesOrderId;
  }

  private deleteSalesOrder() {
    try {
      this.salesOrderService._deleteSalesOrderById(this.salesOrderModel.id)
        .then(result => {
          if (result.status == 1) {

            this.lstSalesOrders.splice(this.lstSalesOrders.findIndex(x => x.id == result.data), 1);

            if (this.lstSalesOrders.length > 0)
              this.getSalesOrderById(this.lstSalesOrders[0].id);

            this.message.text = Global.UI_DELETE_SUCCESS.replace('{0}', 'Sales Order');
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
    var printContent = this.salesOrderModel.salesOrderHtml.replace(`class="Custom-tbody-sidebar"`, "");
    let printWindow = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    printWindow.document.write(`<html>
                                          <head>
                                              <title>${this.salesOrderModel.salesOrderNumber}</title>
                                              <link href= "Content/css/bootstrap.css" rel= "stylesheet" />
                                              <link href="Content/css/Site.css" rel= "stylesheet" />    
                                          </head>
                                          <body onload="window.print()">
                                              ${printContent}
                                          </body>
                                      </html>`);
    printWindow.document.close();
  }

  private getPrintHtml() {
    return this.salesOrderModel.salesOrderHtml.replace(`class="Custom-tbody-sidebar"`, "");
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
          this.removeSalesOrderModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });   
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
