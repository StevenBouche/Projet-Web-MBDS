import { RouteInfo } from "./sidebar.types";

export const ROUTES: RouteInfo[] = [
  {
    path: '/courses/list',
    title: 'Courses',
    icon: 'bi bi-person-workspace',
    class: '',
    extralink: false,
    submenu: []
  },
  {
    path: '/assignments',
    title: 'Assignments',
    icon: 'bi bi-card-checklist',
    class: '',
    extralink: false,
    submenu: []
  }
];
