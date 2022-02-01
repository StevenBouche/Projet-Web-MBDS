import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkProfessorComponent } from './work-professor.component';

describe('WorkProfessorComponent', () => {
  let component: WorkProfessorComponent;
  let fixture: ComponentFixture<WorkProfessorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkProfessorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkProfessorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
