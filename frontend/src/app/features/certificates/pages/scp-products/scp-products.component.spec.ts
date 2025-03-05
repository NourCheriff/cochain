import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScpProductsComponent } from './scp-products.component';

describe('ScpProductsComponent', () => {
  let component: ScpProductsComponent;
  let fixture: ComponentFixture<ScpProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScpProductsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScpProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
