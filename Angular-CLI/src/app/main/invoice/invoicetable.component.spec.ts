import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoicetableComponent } from './invoicetable.component';

describe('InvoicetableComponent', () => {
  let component: InvoicetableComponent;
  let fixture: ComponentFixture<InvoicetableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvoicetableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvoicetableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
