import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesordertableComponent } from './salesordertable.component';

describe('SalesordertableComponent', () => {
  let component: SalesordertableComponent;
  let fixture: ComponentFixture<SalesordertableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesordertableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesordertableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
