import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScpCertificatesComponent } from './scp-certificates.component';

describe('ScpCertificatesComponent', () => {
  let component: ScpCertificatesComponent;
  let fixture: ComponentFixture<ScpCertificatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScpCertificatesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScpCertificatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
