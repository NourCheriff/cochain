import { TestBed } from '@angular/core/testing';

import { ScpProductsService } from './scp-products.service';

describe('ScpProductsService', () => {
  let service: ScpProductsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScpProductsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
