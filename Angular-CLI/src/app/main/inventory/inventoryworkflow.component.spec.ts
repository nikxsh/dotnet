import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryworkflowComponent } from './inventoryworkflow.component';

describe('InventoryworkflowComponent', () => {
  let component: InventoryworkflowComponent;
  let fixture: ComponentFixture<InventoryworkflowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryworkflowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryworkflowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
