import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryGrantFormComponent } from './inventory-grant-form.component';

describe('InventoryGrantFormComponent', () => {
  let component: InventoryGrantFormComponent;
  let fixture: ComponentFixture<InventoryGrantFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InventoryGrantFormComponent]
    });
    fixture = TestBed.createComponent(InventoryGrantFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
