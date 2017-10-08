//core
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { distinctUntilChanged } from 'rxjs/operator/distinctUntilChanged';
import { debounceTime } from 'rxjs/operator/debounceTime';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operator/map';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { MessageHandler, ProductPagingRequest, Filter, PagingRequest } from '../../Models/common.model';
import { InventoryWorkflow, Stock } from '../../models/inventory.model';
import { Product } from '../../models/product.model';
//Service
import { LocalStorageService } from '../../services/storage.service';
import { InventoryService } from '../../services/inventory.service';
import { ProductService } from '../../services/product.service';
//Helper
import * as Global from '../../global'
import { getUoms } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-manageinventory',
  templateUrl: './manageinventory.component.html',
  styleUrls: ['./manageinventory.component.css']
})
export class ManageInventoryComponent implements OnInit {

  @ViewChild("AddEditInputForm")
  addEditInputForm: FormControl

  @ViewChild("AddEditOutputForm")
  addEditOutputForm: FormControl

  private addEditWIPWorkflowModalRef: BsModalRef;

  private productDataSource: any;
  private productTypeaheadNoResults: boolean;
  private productTypeaheadLoading: boolean;
  private selectedProduct: string;

  private workflowDataSource: any;
  private workflowTypeaheadNoResults: boolean;
  private workflowTypeaheadLoading: boolean;
  
  private title: string = "Manage Inventory";
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();
  private lstInventoryWorkflows: InventoryWorkflow[];
  private inventoryWorkflowModel: InventoryWorkflow;
  private lstProductHeads: Array<Product> = [];
  private lstWorkflowHeads: Array<InventoryWorkflow> = [];
  private issueWorkflowWIP: boolean = false;

  private getUoms = getUoms();

  constructor(
    private router: Router,
    private storageService: LocalStorageService,
    private inventoryService: InventoryService,
    private productService: ProductService,
    private modalServiceRef: BsModalService) {
    this.lstInventoryWorkflows = [];
    this.inventoryWorkflowModel = new InventoryWorkflow();

    this.productDataSource = Observable
    .create((observer: any) => {
      // Runs on every search
      observer.next(this.selectedProduct);
    })
    .mergeMap((token: string) => this.getProductInfo(token));

    
    this.workflowDataSource = Observable
    .create((observer: any) => {
      // Runs on every search
      observer.next(this.inventoryWorkflowModel.name);
    })
    .mergeMap((token: string) => this.getWorkflows(token));
  }

  ngOnInit() {
    this.message.text = '';
    this.getAllInventoryWorkflows();
  }

  private getAllInventoryWorkflows() {
    this.message.text = '';
    try {
      this.inventoryService._getAllInventoryWorkflowsWIP()
        .then(result => {
          if (result.status == 1) {
            this.lstInventoryWorkflows = result.data;
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

  private issueWorkflow(id = undefined) {
    this.message.text = '';
    this.issueWorkflowWIP = true;
    Object.assign(this.inventoryWorkflowModel, this.mapWorkflowObject(this.lstInventoryWorkflows.find(x => x.id == id)));
  }

  private saveWorkflow() {
    this.message.text = '';
    try {
      this.progressing = true;
      if (!this.issueWorkflowWIP) {
        this.inventoryService._addInventoryWorkflowsWIP(this.inventoryWorkflowModel)
          .then(result => {
            if (result.status == 1) {
              this.inventoryWorkflowModel.id = result.data;
              this.lstInventoryWorkflows.push(this.mapWorkflowObject(this.inventoryWorkflowModel));
              this.addEditWIPWorkflowModalRef.hide();
              this.message.isGlobal = true;
              this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Inventory Workflow WIP');
              this.message.type = 1;
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
            this.progressing = false;
          },
          error => {
            HandleError.handle(error);
          });
      }
      else {
        this.inventoryService._editInventoryWorkflowsWIP(this.inventoryWorkflowModel)
          .then(result => {
            if (result.status == 1) {
              this.lstInventoryWorkflows.splice(this.lstInventoryWorkflows.findIndex(x => x.id == this.inventoryWorkflowModel.id), 1);
              this.addEditWIPWorkflowModalRef.hide();
              this.message.isGlobal = true;
              this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Inventory Workflow WIP');
              this.message.type = 1;
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
            this.progressing = false;
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

  private getProductInfo(token): Observable<any> {
    let query = new RegExp(token, 'ig');
    try {
      var request = new ProductPagingRequest();
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

  public productTypeheadOnSelect(e: TypeaheadMatch, index, isInputProduct = true): void {
    try {
      if (isInputProduct) {
        let product = this.inventoryWorkflowModel.inputProducts[index];
        product.id = e.item.id;
        product.name = e.item.productName;
        product.code = e.item.skuCode;
        product.uom = e.item.uom;
      }
      else {
        let product = this.inventoryWorkflowModel.outputProducts[index];
        product.id = e.item.id;
        product.name = e.item.productName;
        product.code = e.item.skuCode;
        product.uom = e.item.uom;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
  
  private getWorkflows(token) : Observable<any> {
    let query = new RegExp(token, 'ig');
    try {
      var request = new PagingRequest();
      request.filter.push(new Filter("name", token));
      request.isEnable = true;

      this.inventoryService._getInventoryWorkflowsTypeHeads(request)
        .then(result => {
          if (result.status == 1)
            this.lstWorkflowHeads = result.data;
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
    return Observable.of(this.lstWorkflowHeads.filter((workflow: InventoryWorkflow) => {
      return query.test(workflow.name);
    }));
  }

  public changeworkflowTypeaheadLoading(e: boolean): void {
    this.workflowTypeaheadLoading = e;
  }

  public changeworkflowTypeaheadNoResults(e: boolean): void {
    this.workflowTypeaheadNoResults = e;
  }

  public workflowTypeheadOnSelect(e: TypeaheadMatch): void {
    try {
      this.issueWorkflowWIP = false;
      this.getWorkflowById(e.item.id);
    }
    catch (e) {
      HandleError.handle(e);
    }
  } 

  private addItem(isInputProduct = true) {
    try {
      if (this.addEditInputForm.valid || this.inventoryWorkflowModel.inputProducts.length == 0)
        if (isInputProduct)
          this.inventoryWorkflowModel.inputProducts.push(new Stock());
        else
          this.inventoryWorkflowModel.outputProducts.push(new Stock());
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private removeItem(index, isInputProduct = true) {
    try {
      if (isInputProduct)
        this.inventoryWorkflowModel.inputProducts.splice(index, 1);
      else
        this.inventoryWorkflowModel.outputProducts.splice(index, 1);

    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getWorkflowById(workflowId) {
    try {
      this.inventoryService._getInventoryWorkflowById(workflowId)
        .then(result => {
          if (result.status == 1)
            Object.assign(this.inventoryWorkflowModel, this.mapWorkflowObject(result.data));
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
    return this.lstWorkflowHeads;
  }

  private mapWorkflowObject(source: InventoryWorkflow): InventoryWorkflow {
    var workflow = new InventoryWorkflow();
    workflow.id = source.id;
    workflow.name = source.name;
    workflow.inputProducts = source.inputProducts.map(x => new Stock(x.id, x.name, x.code, x.quantity, x.uom));
    workflow.outputProducts = source.outputProducts.map(x => new Stock(x.id, x.name, x.code, x.quantity, x.uom));
    return workflow;
  }

  public openModal(template: TemplateRef<any>) {
    try {
      this.addEditWIPWorkflowModalRef = this.modalServiceRef.show(template, { class: 'modal-lg' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
