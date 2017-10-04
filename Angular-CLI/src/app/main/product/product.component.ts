//core
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { Product, ProductCategory } from '../../models/product.model';
import { KeyValuePair, MessageHandler, PagingRequest } from '../../Models/common.model';
//Services
import { ProductService } from '../../services/product.service';
//Utility
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';
import { getProductTypes, getTaxSlabs, getUoms } from "../../helpers/common.utility";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit { 
  private productModalRef: BsModalRef;
  private title: string = "Products";
  private switchType: number;
  private isNewProduct: boolean = false;
  private lstProducts: Product[] = [];
  private productModel: Product;
  private pager: any = {};
  private pagedProducts: Product[] = [];
  private lstUOMs: KeyValuePair[] = [];
  private lstProductCategories: ProductCategory[] = [];
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();

  constructor(
      private productServiceRef: ProductService,
      private modalServiceRef: BsModalService) {
      this.productModel = new Product();
      this.message.text = "";
  };

  ngOnInit(): void {
      try {
          this.getProducts();
          this.getProductCategories();
          this.lstUOMs = this.getUoms.filter(x => x.key == 0);
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private getProductTypes = getProductTypes();

  private getUoms = getUoms();

  private getTaxSlabs = getTaxSlabs();

  private getProducts() {
      try {
          this.message.text = '';
          var request = new PagingRequest(this.pager.startIndex, this.pager.endIndex + 1);
          this.productServiceRef._getProducts(request)
              .then(result => {
                  if (result.status == 1) {
                      this.lstProducts = result.data
                      if (result.data.length == 0) {
                          this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Products');
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

  private addProduct() {
      this.isNewProduct = true;
      this.message.text = "";
      this.onReset();
  }
  
  private editProduct(productId) {
      try {
          this.message.text = "";
          this.isNewProduct = false;
          this.resetValues();
          let editedProduct = Object.assign({}, this.lstProducts.find(x => x.id == productId));
          editedProduct.taxSlab = editedProduct.taxSlab;
          Object.assign(this.productModel, editedProduct);
          this.cascadeUOM(this.getProductTypes.find(x => x.key == editedProduct.productType).key);
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private saveProduct() {
      try {
          this.message.isGlobal = false;
          this.message.text = "";

          let category = this.lstProductCategories.find(x => x.name == this.productModel.categoryName);
          if (category != undefined) {
              this.productModel.categoryId = category.id == undefined ? null : category.id;
              this.productModel.categoryName = category.name == undefined ? null : category.name;
          }

          this.progressing = true;
          if (this.isNewProduct) {
              this.productServiceRef._addProduct(this.productModel)
                  .then(result => {
                      this.progressing = false;
                      if (result.status == 1) {
                          this.lstProducts.unshift(result.data); 
                          this.productModalRef.hide();
                          this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Product');
                          this.message.type = 1;
                          this.message.isGlobal = true;
                      }
                      else {
                          this.message.text = result.message;
                          this.message.type = 2;
                      }
                  },
                  error => {
                      this.progressing = false;
                      HandleError.handle(error);
                  });
          }
          else {
              this.productServiceRef._editProduct(this.productModel)
                  .then(result => {
                      this.progressing = false;
                      if (result.status == 1) {
                          Object.assign(this.lstProducts.find(x => x.id == result.data.id), result.data);   
                          this.productModalRef.hide();
                          this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Product');
                          this.message.type = 1;
                          this.message.isGlobal = true;
                      }
                      else {
                          this.message.text = result.message;
                          this.message.type = 2;
                      }
                  },
                  error => {
                      this.progressing = false;
                      HandleError.handle(error);
                  });
          }
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private resetValues() {
      try {
          this.productModel.productType = this.getProductTypes[0].key;
          this.productModel.uom = this.getUoms.filter(x => x.key == 0)[0].value;
          
          this.productModel.taxSlab =  +this.getTaxSlabs[0].value;
          this.productModel.categoryName = this.lstProductCategories[0].name;
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private onProductTypeSelect(type: string) {
      try {
          let key = Number.parseInt(type.split(":")[0].trim());
          this.cascadeUOM(key);
          this.productModel.uom = this.getUoms.filter(x => x.key == key)[0].value;
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private cascadeUOM(key) {
      try {
          this.lstUOMs = this.getUoms.filter(x => x.key == key);
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private getProductCategories() {
      try {
          this.message.text = "";
          this.message.isGlobal = false;

          this.productServiceRef._getProductCategories(false)
              .then(result => {
                  if (result.status == 1) {
                      let categories = result.data.map(x => new ProductCategory(x.id, x.name, x.isEnable));
                      if (categories.length != 0)
                          this.lstProductCategories = categories;
                      else {
                          //this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Product Categories are');
                          this.message.type = 2;
                          this.message.isGlobal = true;
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
  
  private enableDisableProduct(productId) {
      try {
          this.message.text = "";
          this.message.isGlobal = true;

          let product = this.lstProducts.find(x => x.id == productId);

          this.productServiceRef._enableDisableProduct(!product.isEnable, product.id)
              .then(result => {
                  if (result.status == 1) {
                      product.isEnable = !product.isEnable;
                      var status = product.isEnable ? 'Enabled' : 'Disabled';
                      this.message.text = Global.UI_ENABLED_DISABBLED_SUCCESS.replace('{0}', 'Product').replace('{1}', status);
                      this.message.type = 1;
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

  private showAddProductWindow() {
      this.isNewProduct = true;
  }

  private onReset(productId: string = undefined) {
      try {
          this.message.text = "";
          if (productId == undefined) {
              Object.assign(this.productModel, new Product());
              this.resetValues();
          }
          else
              this.editProduct(productId);
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  public openModal(template: TemplateRef<any>) {
    try {
      this.productModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
