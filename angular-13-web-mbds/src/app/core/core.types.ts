export interface Picture{
  id?: number;
  file: File;
  buffer: string | ArrayBuffer | null;
}

export interface ProgressUpload{
  value: number;
  filename: string;
}

export type ProgressAction = (item: ProgressUpload) => void;
