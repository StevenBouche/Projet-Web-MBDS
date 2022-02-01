import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComponentsModule } from 'app/component/component.module';
import { ComponentStateService } from 'app/core/componentstate/componentstate.service';
import { ComponentState } from 'app/core/componentstate/componentstate.types';
import { Picture, ProgressUpload } from 'app/core/core.types';
import { CoursesService } from 'app/core/courses/courses.service';
import { ImageHelper } from 'app/core/helpers/image.helper';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-course-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CourseCreateComponent implements OnInit, OnDestroy {

  public form: FormGroup;
  public image: Picture | null = null;
  public progress: ProgressUpload | null = null;
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  get canCreate() { return this.form.invalid || this.image === null || this.form.disabled; }

  constructor(
    private _formBuilder: FormBuilder,
    private _coursesService: CoursesService,
    private _stateService: ComponentStateService,
    private toast: ToastrService,
    private imageHelper: ImageHelper
  ) {
    this.form = this._formBuilder.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }

  ngOnDestroy(): void {

  }

  ngOnInit(): void {
    this._stateService.setState(ComponentState.Create);
  }

  async selectFiles(event: any): Promise<void> {
    const selected: FileList = event.target.files;
    let file: File = selected[0];
    if (file.size > 0) {
      const mimeType = file.type;
      if (mimeType.match(/image\/(jpe?g|png|gif|bmp)/) !== null) {
        const buffer = await this.imageHelper.getBase64(file);
        this.image = { file: file, buffer: buffer };
        this.progress = { value: 0, filename: file.name };
      }
    }
  }

  async create(): Promise<void> {

    if (this.canCreate) return;

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
  }
}
