import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseordertableComponent } from './purchaseordertable.component';

describe('PurchaseordertableComponent', () => {
  let component: PurchaseordertableComponent;
  let fixture: ComponentFixture<PurchaseordertableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseordertableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseordertableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
