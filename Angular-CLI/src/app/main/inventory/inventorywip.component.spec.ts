import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventorywipComponent } from './inventorywip.component';

describe('InventorywipComponent', () => {
  let component: InventorywipComponent;
  let fixture: ComponentFixture<InventorywipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventorywipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventorywipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
