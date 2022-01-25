import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/auth.guard';


import { FullComponent } from './layouts/full/full.component';

export const Approutes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
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
        path:"home", loadChildren: () => import('./assignments/assignments.module').then(m => m.AssignmentsModule)
      },
      {
        path:"add", loadChildren: () => import('./assignments/add-assignment/add-assignment.component').then(m => m.AddAssignmentComponent)
      },
      {
        path:"assignment/:id", loadChildren: () => import('./assignments/assignment-detail/assignment-detail.component').then(m => m.AssignmentDetailComponent)
      },
      {
        path:"assignment/:id/edit", loadChildren: () => import('./assignments/edit-assignment/edit-assignment.component').then(m => m.EditAssignmentComponent),
        canActivate : [AuthGuard]
      },
    ]
  },
  {
    path: '**',
    redirectTo: '/starter'
  }
];
