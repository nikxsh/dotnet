import { Component, OnInit, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-producttable',
  templateUrl: './producttable.component.html',
  styleUrls: ['./producttable.component.css']
})
export class ProductTableComponent implements OnInit {

  @Input() products: Product[];
  @Input() modal: TemplateRef<any>;
  @Input() flag: boolean = true;

  @Output() OnEditClick: EventEmitter<string> = new EventEmitter();
  @Output() OnEnableDisableClick: EventEmitter<string> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }
  private edit(id) {
    this.OnEditClick.emit(id);
  }

  private enableDisable(id) {
    this.OnEnableDisableClick.emit(id);
  }
}
