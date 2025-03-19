import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewWorkDialogComponent } from './new-work-dialog.component';

describe('NewWorkDialogComponent', () => {
  let component: NewWorkDialogComponent;
  let fixture: ComponentFixture<NewWorkDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewWorkDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewWorkDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
