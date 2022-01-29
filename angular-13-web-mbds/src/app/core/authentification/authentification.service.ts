/* eslint-disable @typescript-eslint/member-ordering */

import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable} from 'rxjs';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { ApiService } from '../api/api.service';
import { LoginForm, LoginResult, UserIdentity } from './auth.types';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService extends ApiService {

    private store: {
        isAuth: boolean;
        identity: UserIdentity | null;
      } = {
        isAuth: false,
        identity: null
      };

  private _isAuth = new BehaviorSubject<boolean>(this.store.isAuth);
  private _identity = new BehaviorSubject<UserIdentity | null>(this.store.identity);

  readonly isAuth = this._isAuth.asObservable();
  readonly identity = this._identity.asObservable();

  private readonly keyStorage = 'token';
  private readonly serviceName = 'Authentification';

  constructor(http: HttpClient, toast: ToastrService, private router: Router) {
    super(http, toast);
  }

  getName(): string {
    return this.serviceName;
  }

   set accessToken(token: string)
   {
       localStorage.setItem(this.keyStorage, token);
   }

   get accessToken(): string
   {
       return localStorage.getItem(this.keyStorage) ?? '';
   }

  public login(login: LoginForm): void{
    this.executePost(
      `${environment.apiBaseUrl}/auth/token`,
      login,
      this.handleLogin,
      error => console.error(error)
    );
  }

  public async loginAsync(login: LoginForm): Promise<void> {

    const result = await this.executePostAsync<LoginForm, LoginResult>(`${environment.apiBaseUrl}/auth/token`, login);

    if (result === null){
        return;
    }

    this.setLocalStorageAsync(this.keyStorage, result);
    this.toast.success('Successful', 'Login');
    await this.getIdentityAsync();
  }

  public logout(): void{
    this.removeLocalStorage(this.keyStorage);
    this.store.identity = null;
    this._identity.next(null);
  }

  public getAuth(): LoginResult | null {
    return this.getLocalStorage<LoginResult>(this.keyStorage);
  }

  public getIsAuth(): boolean {
    return this.store.isAuth;
  }

  public getIdentityObservable(): Observable<UserIdentity> {
    return this.http.get<UserIdentity>(`${environment.apiBaseUrl}/user/identity`).pipe(
      tap((user) => {
          this.setIdentity(user);
      })
    );
  }

  public getIdentity(): void{
    this.executeGet(
      `${environment.apiBaseUrl}/users/identity`,
      (result: UserIdentity) => this.setIdentity(result),
      error => console.error(error)
    );
  }

  public async getIdentityAsync(): Promise<void>{
      const result = await this.executeGetAsync<UserIdentity>(`${environment.apiBaseUrl}/users/identity`);
      this.setIdentity(result);
  }

  private handleLogin(result: LoginResult): void {
    this.setLocalStorageAsync(this.keyStorage, result);
    this.toast.success('Successful', 'Login');
  }

  private setIdentity(result: UserIdentity | null): void{
    this.store.identity = Object.assign({}, result);
    this._identity.next(this.store.identity);
    this.setIsAuth(result !== undefined && result !== null);
  }

  private setIsAuth(is: boolean): void{
    this.store.isAuth = is;
    this._isAuth.next(Object.assign({}, is));
  }

  private setLocalStorageAsync<T>(key: string, obj: T): void {
    localStorage.setItem(key, JSON.stringify(obj));
  }

  private removeLocalStorage(key: string): void {
    localStorage.removeItem(this.keyStorage);
    this.setIdentity(null);
  }

  private getLocalStorage<T>(key: string): T | null {
    const json = localStorage.getItem(key);

    if(json === null)
      return null;

    try {
      //try parse json
      const parse = JSON.parse(json);
      //try cast json object to object T
      return parse as T;
    } catch (error) {
      //on fail parse or cast
      console.error('Error when try to parse or cast local storage object');
      console.error(error);
      return null;
    }
  }

  public async isAuthenticatedAsync(): Promise<boolean> {

    const auth = this.getAuth();
    const currentTime = Date.now();

    //if no tokens store
    if(auth === null){
        return false;
    }

    //if jwt token exist and expiration is valid
    if (auth.jwtToken.expireAt > currentTime ) {
      if (this.store.identity === undefined || this.store.identity === null) {
        await this.getIdentityAsync();
      }
      return true;
    }

    //no valid tokens (jwt and refresh) no auth found
    this.removeLocalStorage(this.keyStorage);
    return false;
  }
}
