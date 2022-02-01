import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { ComponentStateService } from "app/core/componentstate/componentstate.service";
import { Picture, ProgressUpload } from "app/core/core.types";
import { UsersService } from "app/core/users/users.service";
import { User } from "app/core/users/users.types";
import { getBase64 } from "app/core/pictures/pictures.utils";
import { ToastrService } from "ngx-toastr";
import { UserIdentity } from "app/core/authentification/auth.types";
import { Subscription } from "rxjs";
import { AuthentificationService } from "app/core/authentification/authentification.service";

@Component({
  selector: "app-user-edit",
  templateUrl: "./edit.component.html",
  styleUrls: ["./edit.component.scss"],
})
export class EditComponent implements OnInit {
  public form!: FormGroup;

  public image: Picture | null = null;
  public progress: ProgressUpload | null = null;
  public user?: UserIdentity | null;
  private subscription$?: Subscription | null;

  get canUpdate() {
    return this.form.invalid || this.form.disabled;
  }

  hide = true;

  constructor(
    private _formBuilder: FormBuilder,
    private _usersService: UsersService,
    private _authService: AuthentificationService,
    private _stateService: ComponentStateService,
    private _route: ActivatedRoute,
    private toast: ToastrService
  ) {
    this.form = this._formBuilder.group({
      name: [""],
      role: [""],
      password: ["", [Validators.required]],
    });
    this.form.controls["name"].disable(), this.form.controls["role"].disable();
  }

  async ngOnInit(): Promise<void> {
    console.log(this.user);
    this.subscription$ = this._authService.identity.subscribe(
      (user) => (this.user = user)
    );

    this.form.patchValue({
      name: this.user?.name,
      role: this.user?.role,
    });
  }

  async update(): Promise<void> {
    if (this.canUpdate) return;

    this.form.disable();

    try {
      const user = this.form.getRawValue();
      user.id = this.user?.id;
      const userUpdated = await this._usersService.updateAsync(user);
      this.toast.success("User is updated");
      if (this.image?.buffer) {
        this._usersService.uploadPicture(
          this.image!.file,
          async (progress: ProgressUpload) => {
            this.progress = progress;
            if (progress.value === 100) this.toast.success("Image is uploaded");
            await this._authService.getIdentityAsync();
            window.location.reload();
            this.form.enable();
            this.form.controls["name"].disable(),
            this.form.controls["role"].disable();
          }
        );
      } else {
        await this._authService.getIdentityAsync();
        this.form.enable();
        this.form.controls["name"].disable(),
        this.form.controls["role"].disable();
      }
    } catch (error) {
      this.form.enable();
      this.form.controls["name"].disable(),
      this.form.controls["role"].disable();

    }
    /*this.advertPicture,
    (progress: ProgressUpload) => this.updateProgressUpload(progress)*/
  }

  async selectFiles(event: any): Promise<void> {
    const selected: FileList = event.target.files;
    let file: File = selected[0];
    if (file.size > 0) {
      const mimeType = file.type;
      if (mimeType.match(/image\/(jpe?g|png|gif|bmp)/) !== null) {
        const buffer = await getBase64(file);
        this.image = { file: file, buffer: buffer };
        this.progress = { value: 0, filename: file.name };
      }
    }
  }

  public sourceImageUser(userId: number, pictureId: number | null) {
    return this._usersService.sourceImageUser(userId, pictureId);
  }
}
