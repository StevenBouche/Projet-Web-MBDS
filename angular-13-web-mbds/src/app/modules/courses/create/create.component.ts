import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CoursesService } from 'app/core/courses/courses.service';
import { ComponentState } from 'app/core/shared/shared.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CourseCreateComponent implements OnInit, OnDestroy {

  form: FormGroup;
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _formBuilder: FormBuilder,
    private _coursesService: CoursesService
  ) {
    this.form = this._formBuilder.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    //this._coursesService.setStateComponent(ComponentState.Create);
  }

  async create(): Promise<void> {

    /*const contact = this.advertForm.getRawValue();

    const advert = await this._advertsService.createAdvertAsync(
        contact,
        this.advertPicture,
        (progress: ProgressUpload) => this.updateProgressUpload(progress)
    );

    if(advert !== undefined)
    {
        this.createSuccess?.emit();
    }*/

  }

}
