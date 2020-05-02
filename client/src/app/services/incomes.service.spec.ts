import { TestBed } from '@angular/core/testing';

import { IncomesService } from './incomes.service';

describe('IncomesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IncomesService = TestBed.get(IncomesService);
    expect(service).toBeTruthy();
  });
});
