import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogEditFormComponent } from './catalog-edit-form.component';

describe('CatalogEditFormComponent', () => {
  let component: CatalogEditFormComponent;
  let fixture: ComponentFixture<CatalogEditFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CatalogEditFormComponent]
    });
    fixture = TestBed.createComponent(CatalogEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
