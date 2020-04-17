import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  MatMenuModule,
  MatToolbarModule,
  MatIconModule,
  MatButtonModule,
  MatInputModule,
  MatFormFieldModule,
  MatDividerModule,
  MatRadioModule,
  MatTabsModule,
  MatSnackBarModule,
} from "@angular/material";
import { PublicPagesRoutingModule } from "./public-pages-routing.module";
import { PublicPagesComponent } from "./public-pages.component";
import { UserLoginComponent } from "./user-login/user-login.component";
import { HomeComponent } from "./home/home.component";
import { AboutComponent } from "./about/about.component";
import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { CoreModule } from "../core/core.module";
import { LoginComponent } from "./user-login/login/login.component";
import { RegisterComponent } from "./user-login/register/register.component";
import { FlexLayoutModule } from "@angular/flex-layout";

@NgModule({
  declarations: [
    PublicPagesComponent,
    UserLoginComponent,
    HomeComponent,
    AboutComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatMenuModule,
    PublicPagesRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatDividerModule,
    MatFormFieldModule,
    CoreModule,
    MatTabsModule,
    MatSnackBarModule,
  ],
})
export class PublicPagesModule {}
