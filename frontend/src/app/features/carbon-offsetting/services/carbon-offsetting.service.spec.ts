import { TestBed } from '@angular/core/testing';

import { CarbonOffsettingService } from './carbon-offsetting.service';

describe('CarbonOffsettingService', () => {
  let service: CarbonOffsettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarbonOffsettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
