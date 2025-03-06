import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaCertificatesComponent } from './ca-certificates.component';

describe('CaCertificatesComponent', () => {
  let component: CaCertificatesComponent;
  let fixture: ComponentFixture<CaCertificatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CaCertificatesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CaCertificatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
