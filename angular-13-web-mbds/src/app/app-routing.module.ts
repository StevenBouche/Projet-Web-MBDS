import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FullComponent } from './layouts/full/full.component';
import { NoAuthGuard } from './core/guards/noAuth.guard';
import { AuthGuard } from './core/guards/auth.guard';
import { EmptyLayoutComponent } from './layouts/empty/empty.component';

export const Approutes: Routes = [

  // Redirect empty path to '/dashboards/project'
  { path: '', redirectTo: '/courses', pathMatch: 'full' },
  { path: 'signed-in-redirect', pathMatch : 'full', redirectTo: '/courses' },
  // Auth routes for guests
  {
    path: '',
    canActivate: [NoAuthGuard],
    canActivateChild: [NoAuthGuard],
    component: EmptyLayoutComponent,
    children: [
      { path: 'sign-in', loadChildren: () => import('app/modules/authentification/sign-in/sign-in.module').then(m => m.AuthSignInModule) },
      { path: 'sign-up', loadChildren: () => import('app/modules/authentification/sign-up/sign-up.module').then(m => m.AuthSignUpModule) }
    ]
  },
  {
    path: '',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    component: EmptyLayoutComponent,
    data: {
        layout: 'empty'
    },
    children: [
        {path: 'sign-out', loadChildren: () => import('app/modules/authentification/sign-out/sign-out.module').then(m => m.AuthSignOutModule)},
    ]
},
  // Auth routes for logged user
  {
    path: '',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    component: FullComponent,
    children: [
        {
          path: 'courses',
          loadChildren: () => import('app/modules/courses/courses.module').then(m => m.CoursesModule)
        },
        {
          path: 'dashboard',
          loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
        },
        {
          path: 'about',
          loadChildren: () => import('./about/about.module').then(m => m.AboutModule)
        },
        {
          path: 'component',
          loadChildren: () => import('./component/component.module').then(m => m.ComponentsModule)
        },
        {
          path: "home", loadChildren: () => import('./assignments/assignments.module').then(m => m.AssignmentsModule)
        },
        {
          path: "add", loadChildren: () => import('./assignments/add-assignment/add-assignment.component').then(m => m.AddAssignmentComponent)
        },
        {
          path: "assignment/:id", loadChildren: () => import('./assignments/assignment-detail/assignment-detail.component').then(m => m.AssignmentDetailComponent)
        },
        {
          path: "assignment/:id/edit", loadChildren: () => import('./assignments/edit-assignment/edit-assignment.component').then(m => m.EditAssignmentComponent)
        },
    ]
  },
  {
    path: '**',
    redirectTo: '/starter'
  }
];
