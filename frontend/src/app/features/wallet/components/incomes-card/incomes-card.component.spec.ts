import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomesCardComponent } from './incomes-card.component';

describe('IncomesCardComponent', () => {
  let component: IncomesCardComponent;
  let fixture: ComponentFixture<IncomesCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncomesCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomesCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
