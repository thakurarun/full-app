import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [
  {
    path: "public",
    loadChildren: () =>
      import("./public/public-pages.module").then(m => m.PublicPagesModule)
  },
  {
    path: "",
    redirectTo: "public/home",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
