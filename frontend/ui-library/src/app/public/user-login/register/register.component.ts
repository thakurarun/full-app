import { Component, OnInit } from "@angular/core";
import { HttpErrorResponse } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import {
  FormBuilder,
  FormControl,
  ValidatorFn,
  Validators,
  AbstractControl,
  ValidationErrors,
  FormGroup
} from "@angular/forms";
import { of, BehaviorSubject } from "rxjs";
import { IFieldItem } from "../field-item";
import { UserLoginService } from "../user-login.service";
import { MatSnackBar, MatSnackBarConfig } from "@angular/material";
@Component({
  selector: "app-user-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
  fields: IFieldItem[] = [];
  formGroup = this.fb.group({});
  formError$ = new BehaviorSubject<string | undefined>(undefined);

  constructor(
    private userLoginService: UserLoginService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.fetchLoginFormSchema();
  }

  async registerUser(data: any): Promise<void> {
    this.formError$.next(undefined);
    try {
      const response = await this.userLoginService.registerUser(data);
      this.resetForm();
      this.snackBar.open("User registered successfully.", undefined, {
        duration: 2000
      });
    } catch (error) {
      this.handleError(error);
    }
  }

  handleError(err: HttpErrorResponse): void {
    if (
      err.error.status === 409 &&
      err.error.identifier === "UserAlreadyRegisteredWithEmail"
    ) {
      this.formError$.next("User already exists with this email.");
    } else {
      this.formError$.next("unknown error.");
    }
  }

  private async fetchLoginFormSchema(): Promise<void> {
    const jsonSchema = await this.userLoginService.fetchRegisterFormSchema();
    this.fields = this.jsonSchemaToFormProperties(jsonSchema);
    this.formGroup.validator = this.comparePassword.bind(this);
  }

  private jsonSchemaToFormProperties(jsonSchema: Object): IFieldItem[] {
    const requiredFields = jsonSchema["required"] as string[];
    const fields = jsonSchema["properties"];
    return Object.keys(fields).map(key => {
      const field = fields[key] as IFieldItem;
      if (requiredFields.includes(key)) {
        field["required"] = true;
      }
      if (field instanceof Array) {
        field.type = null;
      }
      field.property = key;
      field.formControl = this.fb.control(
        field.defaultValue,
        this.getValidators(field)
      );
      this.formGroup.addControl(key, field.formControl);
      return field;
    });
  }

  getValidators(field: IFieldItem): ValidatorFn[] {
    let validators: ValidatorFn[] = [];
    if (field.required) {
      validators.push(Validators.required);
    }
    if (field.minLength) {
      validators.push(Validators.minLength(field.minLength));
    }
    if (field.maxLength) {
      validators.push(Validators.maxLength(field.maxLength));
    }
    if (field.format && field.format === "email") {
      validators.push(Validators.email);
      validators.push(
        Validators.pattern(
          /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
        )
      );
    }
    return validators;
  }

  comparePassword(): ValidationErrors | null {
    let confirmPsw = this.f("confirmPassword");
    let psw = this.f("password");
    if (confirmPsw.dirty || psw.dirty || confirmPsw.touched || psw.touched) {
      return confirmPsw.value === psw.value ? null : { notMatched: true };
    }
    return null;
  }

  resetForm(): void {
    this.formError$.next(undefined);
    this.formGroup.reset();
    this.formGroup.markAsUntouched();
  }

  getErrorMessage(ctrl: FormControl | FormGroup): string | null {
    if (!ctrl.errors) {
      return null;
    }
    return Object.keys(ctrl.errors).map(key => {
      switch (key.toLowerCase()) {
        case "required":
          return "Field is required.";
        case "maxlength":
          return "Maximum characters reached.";
        case "minlength":
          return "Need some more characters.";
        case "email":
        case "pattern":
          return "Email address is not a valid.";
        case "notmatched":
          return "Password and confirm password doesn't match.";
      }
    })[0];
  }

  f(name: string): AbstractControl {
    return this.formGroup.get(name);
  }
}
