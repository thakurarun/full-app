import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class UserLoginService {
  constructor(private http: HttpClient) {}

  registerUser(data: any): Promise<any> {
    return this.http.post("/api/signup", data).toPromise();
  }

  fetchRegisterFormSchema(): Promise<any> {
    return this.http.get("/api/signup").toPromise();
  }
}
