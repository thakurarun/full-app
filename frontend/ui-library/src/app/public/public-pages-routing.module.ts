import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { PublicPagesComponent } from "./public-pages.component";
import { HomeComponent } from "./home/home.component";
import { AboutComponent } from "./about/about.component";
import { UserLoginComponent } from "./user-login/user-login.component";

const routes: Routes = [
  {
    path: "",
    component: PublicPagesComponent,
    children: [
      { path: "home", component: HomeComponent },
      { path: "about", component: AboutComponent },
      { path: "login", component: UserLoginComponent }
    ]
  },
  {
    path: "",
    redirectTo: "home",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicPagesRoutingModule {}
