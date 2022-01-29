export enum ComponentState {
  None,
  List,
  Create,
  Edit,
  Details
}

export interface StateAction {
  view: boolean;
  disabled: boolean;
}
