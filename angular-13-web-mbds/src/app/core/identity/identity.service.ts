import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { ApiService } from "../api/api.service";
import { UserIdentity } from "../authentification/auth.types";

@Injectable({
  providedIn: "root",
})
export class IdentityService {

  private store: {
    identity: UserIdentity | null;
  } = {
    identity: null,
  };

  private _identity = new BehaviorSubject<UserIdentity | null>(this.store.identity);
  readonly identity = this._identity.asObservable();

  constructor() {}

  public getIdentity(): UserIdentity | null {
    return this.store.identity;
  }

  public setIdentity(result: UserIdentity | null): void {
    this.store.identity = result;
    this._identity.next(result);
  }
}
