export interface User
{
  id: number;
  role: string;
  name: string;
  pictureId: number;
}

export interface UserFormUpdate {
  id: number;
  name: string;
  password?: string;
  pictureId: number;
}

export interface UserFormCreate {
  role: string;
  name: string;
  pictureId: number;
}
