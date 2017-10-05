//Core
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { MessageHandler, InventoryPagingRequest, KeyValuePair } from '../../Models/common.model';
import { BaseInventory, Inventory, FilterType, FilterSelection } from '../../models/inventory.model';
//Services
import { LocalStorageService } from '../../services/storage.service';
import { InventoryService } from '../../services/inventory.service';
//Helper
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';
import { getFormattedDate } from '../../helpers/common.utility';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  private inventoryDetailsModalRef: BsModalRef;
  private title: string = "Inventory Dashboard";
  private progressing: false;
  private message: MessageHandler = new MessageHandler();
  private lstInventory: BaseInventory[] = [];
  private inventoryHistory: Inventory[] = [];
  private filterType: FilterType = 0;
  private filterSelection: FilterSelection;
  private currentDate: Date;
  private minDate: string;
  private request: InventoryPagingRequest;

  private months: KeyValuePair[] = [
      new KeyValuePair(1, 'Jan'),
      new KeyValuePair(2, 'Feb'),
      new KeyValuePair(3, 'Mar'),
      new KeyValuePair(4, 'Apr'),
      new KeyValuePair(5, 'May'),
      new KeyValuePair(6, 'Jun'),
      new KeyValuePair(7, 'Jul'),
      new KeyValuePair(8, 'Aug'),
      new KeyValuePair(9, 'Sep'),
      new KeyValuePair(10, 'Oct'),
      new KeyValuePair(11, 'Nov'),
      new KeyValuePair(12, 'Dec'),
  ];


  private years: string[] = [];

  constructor(
      private router: Router,
      private storageService: LocalStorageService,
      private inventoryService: InventoryService,
      private modalServiceRef: BsModalService) {
      this.filterSelection = new FilterSelection();
  }

  ngOnInit() {
      this.message.text = '';
      this.request = new InventoryPagingRequest();    
      this.request.isEnable = undefined;    

      this.currentDate = new Date();
      for (var i = 0; i < 33; i++)
          this.years.push((2017 + i).toString());

      this.filterSelection.month = this.months[this.currentDate.getMonth()].value;
      this.filterSelection.year = this.currentDate.getFullYear().toString();

      this.request.queryType = 0;
      this.request.month = this.months.find(x => x.value == this.filterSelection.month).key;
      this.request.year = +this.filterSelection.year;
      this.request.selectedDate = undefined;
      this.request.dateRangeFrom = undefined;
      this.request.dateRangeTo = undefined;
      this.getAllInventory(this.request);
  }

  private getAllInventory(request: InventoryPagingRequest) {
      try {
          this.message.text = '';
          this.inventoryService._getAllProductInventory(this.request)
              .then(result => {
                  if (result.status == 1) {
                      this.lstInventory = result.data.map(x => new BaseInventory(x.id, x.productCode, x.productName, x.in, x.out, 0, x.available, x.uom, x.date, 0));
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

  private search(type: number) {
      try {                                              
          this.minDate = this.filterSelection.fromDate;

          this.filterType = type;
          if (type == 0 && this.filterSelection.month != undefined && this.filterSelection.year != undefined) {
              this.request.queryType = 0;
              this.request.month = this.months.find(x => x.value == this.filterSelection.month).key;
              this.request.year = +this.filterSelection.year;
              this.request.selectedDate = undefined;
              this.request.dateRangeFrom = undefined;
              this.request.dateRangeTo = undefined;
              this.filterSelection.byDate = undefined;
              this.filterSelection.fromDate = undefined;
              this.filterSelection.toDate = undefined;
              this.getAllInventory(this.request);
          }
          else if (type == 1 && this.filterSelection.byDate != undefined) {
              this.request.queryType = 1;
              this.request.selectedDate = this.filterSelection.byDate;
              this.request.month = undefined;
              this.request.year = undefined;
              this.request.dateRangeFrom = undefined;
              this.request.dateRangeTo = undefined;
              this.filterSelection.month = undefined;
              this.filterSelection.year = undefined;
              this.filterSelection.fromDate = undefined;
              this.filterSelection.toDate = undefined;
              this.getAllInventory(this.request);
          }
          else if (type == 2 && this.filterSelection.fromDate != undefined && this.filterSelection.toDate != undefined) {
              this.request.queryType = 2;
              this.request.dateRangeFrom = this.filterSelection.fromDate;
              this.request.dateRangeTo = this.filterSelection.toDate;
              this.request.selectedDate = undefined;
              this.request.month = undefined;
              this.request.year = undefined;
              this.filterSelection.byDate = undefined;
              this.filterSelection.month = undefined;
              this.filterSelection.year = undefined;
              this.getAllInventory(this.request);
          }
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private getInventoryDetails(id) {
      try {
          this.message.text = '';
          this.request.id = id;

          this.inventoryService._getProductInventoryHistory(this.request)
              .then(result => {
                  if (result.status == 1) {
                      this.inventoryHistory = result.data.map(x => {
                          let item = new Inventory(x.invoiceNumber, x.salesNumber, x.billNumber, x.purchaseNumber, x.purchaseReceiveNumber);
                          item.id = x.id;
                          item.productName = x.productName;
                          item.productCode = x.productCode;
                          item.uom = x.uom;
                          item.available = x.available;
                          item._in = x.in;
                          item.out = x.out;
                          item.date = getFormattedDate(x.date);
                          item.runningTotal = x.runningTotal;
                          return item;
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

  public openModal(template: TemplateRef<any>) {
    try {
      this.inventoryDetailsModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
