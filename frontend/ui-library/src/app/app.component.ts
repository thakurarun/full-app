import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-root",
  template: ` <router-outlet></router-outlet> `,
  styles: [],
})
export class AppComponent implements OnInit {
  title = "ui-library";

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}
}
