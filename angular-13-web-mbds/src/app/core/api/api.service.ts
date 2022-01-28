import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'environments/environment';
import { ApiErrorResponse } from './api.types';

export type ActionService<T> = (item: T) => void;
export type ActionError = (item: any) => void;

@Injectable({
    providedIn: 'root'
})
export abstract class ApiService {

    private log: boolean = false;
    get baseUrl(): string{
        return environment.apiBaseUrl;
    }

    constructor(protected http: HttpClient, protected toast: ToastrService) {

    }

    protected executeGet<K>(url: string, callback: ActionService<K>, error: ActionError): void {
        this.execute(this.http.get<K>(url), callback, error);
    }

    protected executePost<T,K>(url: string, data: T, callback: ActionService<K>, error: ActionError): void{
        this.execute(this.http.post<K>(url, data), callback, error);
    }

    protected executePut<T,K>(url: string, data: T, callback: ActionService<K>, error: ActionError): void{
        this.execute(this.http.put<K>(url,data), callback, error);
    }

    protected executeDelete<K>(url: string, callback: ActionService<K>, error: ActionError): void{
        this.execute(this.http.delete<K>(url), callback, error);
    }

    protected async executeGetAsync<K>(url: string): Promise<K> {
        return this.executeAsync(this.http.get<K>(url));
    }

    protected async executePostAsync<T,K>(url: string, data: T | null): Promise<K> {
        return this.executeAsync(this.http.post<K>(url, data));
    }

    protected async executePutAsync<T,K>(url: string, data: T): Promise<K> {
        return this.executeAsync(this.http.put<K>(url,data));
    }

    protected async executeDeleteAsync<K>(url: string): Promise<K> {
        return this.executeAsync(this.http.delete<K>(url));
    }

    private execute<K>(fun: Observable<K>, callback: ActionService<K>, callbackError: ActionError): void {
        fun.pipe<K>(catchError(this.handleError<K>()))
            .subscribe((result: K) => callback(result), error => callbackError(error));
    }

    private async executeAsync<K>(fun: Observable<K>): Promise<K> {
        return new Promise<K>((resolve,reject) => {
            fun.pipe(catchError(this.handleError<K>()))
            .subscribe({
              next: (result: K) => resolve(result),
              error: (error: any ) => reject(error)
            });
        });
    }

    private handleError<T>() {
        return (error: HttpErrorResponse): Observable<T> => {
            try {
                console.log(error);
                const err: ApiErrorResponse = error.error;
                /*const value = `message:${err.message}, detail: ${err.detail}`;
                console.error(`Error Code: ${error.status}\n${value}`);*/
                this.toast.error(err.message, this.getName());
                return throwError(() => err);
            } catch(errorParse){
                console.error(error);
                return throwError(() => error);
            }
        };
    }

    abstract getName(): string;
}
