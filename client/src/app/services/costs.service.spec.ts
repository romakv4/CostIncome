import { TestBed } from '@angular/core/testing';

import { CostsService } from './costs.service';

describe('CostsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CostsService = TestBed.get(CostsService);
    expect(service).toBeTruthy();
  });
});
