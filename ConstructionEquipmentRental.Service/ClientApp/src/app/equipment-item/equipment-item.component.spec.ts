import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentItemComponent } from './equipment-item.component';

describe('EquipmentItemComponent', () => {
  let component: EquipmentItemComponent;
  let fixture: ComponentFixture<EquipmentItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
