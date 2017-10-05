import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BilltableComponent } from './billtable.component';

describe('BilltableComponent', () => {
  let component: BilltableComponent;
  let fixture: ComponentFixture<BilltableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BilltableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BilltableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
