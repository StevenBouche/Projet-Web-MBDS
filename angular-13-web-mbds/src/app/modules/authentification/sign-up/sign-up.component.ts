import { Component, OnInit, ViewChild, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup, NgForm, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Picture, ProgressUpload } from "app/core/core.types";
import { UsersService } from "app/core/users/users.service";
import { ToastrService } from "ngx-toastr";
import { AuthentificationService } from "app/core/authentification/authentification.service";
import { ImageHelper } from "app/core/helpers/image.helper";

@Component({
  selector: "auth-sign-up",
  templateUrl: "./sign-up.component.html",
  encapsulation: ViewEncapsulation.None,
})
export class AuthSignUpComponent implements OnInit {
  roles: string[] = ["STUDENT", "PROFESSOR"];

  signUpForm: FormGroup;
  showAlert: boolean = false;
  hide = true;
  public image: Picture | null = null;
  public progress: ProgressUpload | null = null;
  get canCreate() {
    return (
      this.signUpForm.invalid || this.image === null || this.signUpForm.disabled
    );
  }

  constructor(
    private _formBuilder: FormBuilder,
    private toast: ToastrService,
    private _usersService: UsersService,
    private _router: Router,
    private _authentificationService: AuthentificationService,
    private imageHelper: ImageHelper
  ) {
    // Create the form
    this.signUpForm = this._formBuilder.group({
      name: ["", Validators.required],
      password: ["", Validators.required],
      role: ["", Validators.required],
    });
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit(): void {}

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

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
  /**
   * Sign up
   */
  signUp(): void {
    // Do nothing if the form is invalid
    if (this.signUpForm.invalid) {
      return;
    }

    // Disable the form
    this.signUpForm.disable();

    // Hide the alert
    this.showAlert = false;
  }

  async signup(): Promise<void> {
    if (this.canCreate) return;

    this.signUpForm.disable();

    try {
      const user = this.signUpForm.getRawValue();
      const userCreated = await this._usersService.createAsync(user);
      this.toast.success("User is created");
      await this._authentificationService.loginAsync({
        name: user.name,
        password: user.password,
      }, false);
      this._usersService.uploadPicture(
        this.image!.file,
        (progress: ProgressUpload) => {
          this.progress = progress;
          if (progress.value === 100) {
            this.toast.success("Image is uploaded");
            (async () => {
              // Do something before delay
              this.toast.success("Redirect to home page");
              await new Promise(f => setTimeout(f, 1000));
              // Do something after
              this._router.navigate(["/"]);
            })();
          }
          this.signUpForm.enable();
        }
      );
    } catch (error) {
      this.signUpForm.enable();
    }
    /*this.advertPicture,
      (progress: ProgressUpload) => this.updateProgressUpload(progress)*/
  }
}
