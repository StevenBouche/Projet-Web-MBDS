export enum ComponentState {
  None = 1,
  List = 2,
  Create = 3,
  Edit = 4,
  Details = 5
}

export interface StateAction {
  view: boolean;
  disabled: boolean;
}
