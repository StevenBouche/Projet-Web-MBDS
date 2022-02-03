import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Work } from 'app/core/works/works.type';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-work-professor-item',
  templateUrl: './work-professor-item.component.html',
  styleUrls: ['./work-professor-item.component.scss']
})
export class WorkProfessorItemComponent implements OnInit {

  @Input() item: Work | null = null;
  @Input() isInEvaluation: boolean | null = false;
  @Output() onSave: EventEmitter<FormGroup> = new EventEmitter();
  @Output() onSubmit: EventEmitter<FormGroup> = new EventEmitter();

  form: FormGroup;



  constructor(

    private _formBuilder: FormBuilder,

  ) {
    this.form = this._formBuilder.group({
      grade: ['', [Validators.required]],
      comment: ['', [Validators.required]],
      id: [''],
    });

  }

  ngOnInit(): void {

    if (this.item) {
      console.log(this.item)
      this.form.patchValue({
        grade: this.item.grade,
        comment: this.item.comment,
        id: this.item.id,
      });
      if(this.item.state === 2){
        this.form.disable();
      }
    }
  }

  onClickSave() {
    if(this.isInEvaluation){
      this.onSubmit.emit(this.form);
    }
    else{
    this.onSave.emit(this.form);
    }
  }

  onClickSubmit() {
    this.onSubmit.emit(this.form);
  }

}
