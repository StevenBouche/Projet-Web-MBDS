import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { Picture, ProgressUpload } from 'app/core/core.types';
import { CoursesService } from 'app/core/courses/courses.service';
import { Course } from 'app/core/courses/courses.type';
import { getBase64 } from 'app/core/pictures/pictures.utils';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-course-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  public form!: FormGroup ;

  public image: Picture | null = null;
  public progress: ProgressUpload | null = null;

  get canUpdate() { return this.form.invalid || this.image === null || this.form.disabled; }

  constructor(
    private _formBuilder: FormBuilder,
    private _coursesService: CoursesService,
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private toast: ToastrService
    ) {

      this.form = this._formBuilder.group({
        id: [ '', [Validators.required]],
        name: [ '', [Validators.required]],
        description: [ '', [Validators.required]]
      });
  }

  async ngOnInit(): Promise<void> {

    const data: Course = this._route.snapshot.data.initialData.course;
    const file: File = this._route.snapshot.data.initialData.file;

    const buffer = await getBase64(file);
    this.image = { file: file, buffer: buffer };

    console.log(this.image)

    this.form.patchValue({
      id: data.id,
      name: data.name,
      description: data.description
    });
  }

  async update(): Promise<void> {

    if (this.canUpdate) return;

    this.form.disable();

    try {
      const course = this.form.getRawValue();
      const courseCreated = await this._coursesService.createAsync(course);
      this.toast.success('Course is created');
      this._coursesService.uploadPicture(courseCreated.id, this.image!.file, (progress: ProgressUpload) => {
        this.progress = progress;
        if (progress.value === 100)
          this.toast.success('Image is uploaded');
        this.form.enable();
      })
    }
    catch (error) {
      this.form.enable();
    }
    /*this.advertPicture,
    (progress: ProgressUpload) => this.updateProgressUpload(progress)*/
  }

}
