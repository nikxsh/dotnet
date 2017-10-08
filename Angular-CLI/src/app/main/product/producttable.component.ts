import { Component, OnInit, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { Product } from '../../models/product.model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-producttable',
  templateUrl: './producttable.component.html',
  styleUrls: ['./producttable.component.css']
})
export class ProductTableComponent implements OnInit {
  productModalRef: BsModalRef;

  @Input() products: Product[];
  @Input() template: TemplateRef<any>;
  @Input() flag: boolean = true;

  @Output() OnEditClick: EventEmitter<any> = new EventEmitter();
  @Output() OnEnableDisableClick: EventEmitter<string> = new EventEmitter();

  constructor(
    private modalServiceRef: BsModalService) { }

  ngOnInit() {
  }
  private edit(id) {    
    try {
      this.productModalRef = this.modalServiceRef.show(this.template, { class: 'modal-lg' });
      this.OnEditClick.emit({ productId: id, modalRef: this.productModalRef });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private enableDisable(id) {
    this.OnEnableDisableClick.emit(id);
  }
}
