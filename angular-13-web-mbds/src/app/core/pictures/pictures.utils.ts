import { environment } from "environments/environment";

export function getBase64(file: File): Promise< string | ArrayBuffer | null> {
  const reader = new FileReader();
  return new Promise((resolve) => {
      reader.onload = (ev: ProgressEvent<FileReader>): void => ev != null && ev.target != null ? resolve(ev.target.result) : resolve(null);
      reader.readAsDataURL(file);
  });
}

export function sourceImageCourse(idpicture: number){
  return idpicture ? `${environment.apiBaseUrl}/courseimages/${idpicture}` : 'assets/images/bg/bg1.jpg';
}

export function sourceImageUser(idpicture: number){
  return idpicture ? `${environment.apiBaseUrl}/userprofilimages/${idpicture}` : 'assets/images/users/user1.jpg';
}
