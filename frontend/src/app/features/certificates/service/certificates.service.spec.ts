import { TestBed } from '@angular/core/testing';

import { CertificatesService } from './certificates.service'

describe('ScpProductsService', () => {
  let service: CertificatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CertificatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
