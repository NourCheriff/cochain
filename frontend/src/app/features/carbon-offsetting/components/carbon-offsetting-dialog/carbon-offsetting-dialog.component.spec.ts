import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarbonOffsettingDialogComponent } from './carbon-offsetting-dialog.component';

describe('CarbonOffsettingDialogComponent', () => {
  let component: CarbonOffsettingDialogComponent;
  let fixture: ComponentFixture<CarbonOffsettingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarbonOffsettingDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarbonOffsettingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
