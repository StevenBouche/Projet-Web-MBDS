export interface LoginForm {
  name: string;
  password: string;
}

export interface JwtToken {
  accessToken: string;
  expireAt: number;
}

export interface RefreshToken {
  refreshToken: string;
  expireAt: number;
}

export interface LoginResult {
  jwtToken: JwtToken;
  refreshToken: RefreshToken;
}

export interface UserIdentity {
  id: number;
  name: string;
  role: string;
  pictureId: number;
  updatedAt: Date;
}


