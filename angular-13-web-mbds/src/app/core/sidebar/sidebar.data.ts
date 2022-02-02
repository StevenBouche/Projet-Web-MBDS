import { RouteInfo } from "./sidebar.types";

export const ROUTES_PROFESSOR : RouteInfo[] = [
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

export const ROUTES_STUDENT : RouteInfo[] = [
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
  },
  {
    path: '/works',
    title: 'Works',
    icon: 'bi bi-card-checklist',
    class: '',
    extralink: false,
    submenu: []
  }
];
