import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseorderComponent } from './purchaseorder.component';

describe('PurchaseorderComponent', () => {
  let component: PurchaseorderComponent;
  let fixture: ComponentFixture<PurchaseorderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseorderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
