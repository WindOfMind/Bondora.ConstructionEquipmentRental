import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentCartComponent } from './equipment-cart.component';

describe('EquipmentCartComponent', () => {
  let component: EquipmentCartComponent;
  let fixture: ComponentFixture<EquipmentCartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentCartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
