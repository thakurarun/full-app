import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  Validators,
  FormGroup,
  AbstractControl
} from "@angular/forms";

@Component({
  selector: "app-user-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  form = this.createLoginForm();

  constructor(private fb: FormBuilder) {}

  ngOnInit() {}

  createLoginForm(): FormGroup {
    return this.fb.group({
      email: new FormControl("", [
        Validators.required,
        Validators.pattern(
          /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
        )
      ]),
      password: new FormControl("", Validators.required)
    });
  }

  login(): void {
    //todo
  }

  f(name: string): AbstractControl {
    return this.form.get(name);
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
}
