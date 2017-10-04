//core
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Router } from '@angular/router';
//Models
import { Product, ProductCategory } from '../../models/product.model';
import { MessageHandler, PagingRequest } from '../../Models/common.model';
//Services
import { LocalStorageService } from '../../services/storage.service';
//Utility
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-productcategories',
  templateUrl: './productcategories.component.html',
  styleUrls: ['./productcategories.component.css']
})
export class ProductCategoriesComponent implements OnInit {
  private productCategoryModalRef: BsModalRef;
  private title: string = "Product Categories";
  private lstProductCategories: ProductCategory[] = [];
  private productCategoryModel: ProductCategory;
  private lstProducts: Product[] = [];
  private showAddCategoryTextbox: boolean = false;
  private addCategoryTextboxText: string = "Add";
  private progressing: boolean = false;
  private isNewCategory: boolean = false;
  private selectedProductCategoryIndex: number = 0;
  private pager: any = {};
  private message: MessageHandler = new MessageHandler();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private localStorageService: LocalStorageService,
    private productService: ProductService,
    private modalServiceRef: BsModalService) {
    this.productCategoryModel = new ProductCategory();
  }

  ngOnInit(): void {
    this.getProductCategories();
  }

  private getProductCategories() {
    try {
      this.message.isGlobal = true;
      this.productService._getProductCategories(false)
        .then(result => {
          if (result.status == 1) {

            let categories = result.data.map(x => new ProductCategory(x.id, x.name, x.isEnable));
            if (categories.length != 0) {
              this.lstProductCategories = categories;
              this.getProductByID(this.lstProductCategories[0].id);
            }
            else {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Product Categories are');
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

  private getProducts() {
    try {
      this.message.isGlobal = true;
      var request = new PagingRequest(this.pager.startIndex, this.pager.endIndex + 1);
      this.productService._getProducts(request)
        .then(result => {
          if (result.status == 1)
            this.lstProducts = result.data;
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

  private addCategory() {
    this.message.text = '';
    this.isNewCategory = true;
    this.productCategoryModel = new ProductCategory();
  }


  private saveProductCategory() {
    try {
      this.progressing = true;
      this.message.isGlobal = false;
      if (this.isNewCategory) {
        this.productService._addProductCategory(this.productCategoryModel)
          .then(result => {
            this.progressing = false;
            if (result.status == 1) {
              let category = Object.assign({}, result.data);
              if (category != undefined) {
                this.lstProductCategories.unshift(category);
                this.lstProducts = [];
              }
              this.productCategoryModalRef.hide();
              this.selectedProductCategoryIndex = 0;
              this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Category');
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
        this.productService._renameProductCategory(this.productCategoryModel)
          .then(result => {
            this.progressing = false;
            if (result.status == 1) {
              Object.assign(this.lstProductCategories.find(x => x.id == result.data.id), result.data);
              this.productCategoryModalRef.hide();
              this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Category');
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

  private onProductCategoryClick(categoryId, index) {
    this.message.text = '';
    this.selectedProductCategoryIndex = index;
    this.getProductByID(categoryId);
  }

  private getProductByID(categoryId) {
    try {
      this.message.isGlobal = true;
      this.productService._getProductsByCategoryId(categoryId)
        .then(result => {
          if (result.status == 1) {
            this.lstProducts = result.data;
            if (this.lstProducts.length == 0) {
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

  private renameCategory(categoryId) {
    this.isNewCategory = false;
    this.message.text = '';
    Object.assign(this.productCategoryModel, this.lstProductCategories.find(x => x.id == categoryId));
  }

  private enableDisableProductCategory(productCategoryId) {
    try {
      this.message.text = '';
      this.message.isGlobal = true;
      let category = this.lstProductCategories.find(x => x.id == productCategoryId);

      this.productService._enableDisableProductCategory(!category.isEnable, category.id)
        .then(result => {
          if (result.status == 1) {
            category.isEnable = !category.isEnable;
            this.getProductByID(category.id);
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

  private onReset(categoryId) {
    try {
      this.message.text = '';
      if (this.isNewCategory) {
        this.productCategoryModel.name = "";
        this.productCategoryModel.id = "";
        this.productCategoryModel.isEnable = false;
      } else
        this.renameCategory(categoryId)
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  public openModal(template: TemplateRef<any>) {
    try {
      this.productCategoryModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

}
