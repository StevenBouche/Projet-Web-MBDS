import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { map, Observable, of, tap } from "rxjs";

interface CachedImage {
  url: string;
  content: string;
}

@Injectable({
  providedIn: "root",
})
export class ImageHelper {

  private _cacheUrls: string[] = [];
  private _cachedImages: CachedImage[] = [];

  set cacheUrls(urls: string[]) { this._cacheUrls = [...urls]; }
  get cacheUrls(): string[] { return this._cacheUrls; }
  set cachedImages(image: CachedImage) { this._cachedImages.push(image); }

  constructor(private http: HttpClient) { }

  private getImage(url: string) : Observable<string> {
    const index = this._cachedImages.findIndex(image => image.url === url);
    if (index > -1) {
      const image = this._cachedImages[index];
      return of(image.content);
    }
    console.log(url)
    return this.http.get(url, { responseType: 'blob' }).pipe(
      tap(blob => this.checkAndCacheImage(url, blob)),
      map(blob => URL.createObjectURL(blob))
    );
  }

  private checkAndCacheImage(url: string, blob: Blob) {
    if (this._cacheUrls.indexOf(url) > -1) {
      this._cachedImages.push({url, content: URL.createObjectURL(blob)});
      console.log(this._cachedImages)
    }
  }

  public getBase64(file: File): Promise<string | ArrayBuffer | null> {
    const reader = new FileReader();
    return new Promise((resolve) => {
      reader.onload = (ev: ProgressEvent<FileReader>): void => ev != null && ev.target != null ? resolve(ev.target.result) : resolve(null);
      reader.readAsDataURL(file);
    });
  }

  public sourceImageCourse(idpicture: number | null) {
   /* if(idpicture == null)
      return of('assets/images/bg/bg1.jpg')
    else
      return this.getImage(`${environment.apiBaseUrl}/courseimages/${idpicture}`)*/
    return idpicture ? `${environment.apiBaseUrl}/courseimages/${idpicture}` : 'assets/images/bg/bg1.jpg';
  }

  public sourceImageCourseByCourseId(courseid: number | null) {
    return courseid ? `${environment.apiBaseUrl}/courseimages/course/${courseid}` : 'assets/images/bg/bg1.jpg';
  }

  public sourceImageUser(idpicture: number | null) {
    return idpicture ? `${environment.apiBaseUrl}/userprofilimages/${idpicture}` : 'assets/images/users/user1.jpg';
  }

  public sourceImageUserByUserId(userid: number | null) {
    return userid ? `${environment.apiBaseUrl}/userprofilimages/user/${userid}` : 'assets/images/users/user1.jpg';
  }

  public sourceImageUserByUserIdIfExist(userId: number, pictureId: number | null) {
    if (!pictureId) {
      return 'assets/images/users/user1.jpg';
    }
    return userId ? `${environment.apiBaseUrl}/userprofilimages/user/${userId}` : 'assets/images/users/user1.jpg';
  }
}
