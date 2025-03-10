import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractsSectionComponent } from './contracts-section.component';

describe('ContractsSectionComponent', () => {
  let component: ContractsSectionComponent;
  let fixture: ComponentFixture<ContractsSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContractsSectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContractsSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
