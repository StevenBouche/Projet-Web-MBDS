import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkStudentComponent } from './work-student.component';

describe('WorkStudentComponent', () => {
  let component: WorkStudentComponent;
  let fixture: ComponentFixture<WorkStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkStudentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
