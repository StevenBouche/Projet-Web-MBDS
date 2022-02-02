import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkProfessorItemComponent } from './work-professor-item.component';

describe('WorkProfessorItemComponent', () => {
  let component: WorkProfessorItemComponent;
  let fixture: ComponentFixture<WorkProfessorItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkProfessorItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkProfessorItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
