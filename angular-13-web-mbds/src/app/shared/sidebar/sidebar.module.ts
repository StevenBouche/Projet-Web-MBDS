import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SidebarCoreModule } from 'app/core/sidebar/sidebar.module';
import { SharedModule } from 'app/shared/shared.module';
import { SidebarComponent } from './sidebar.component';

@NgModule({
    declarations: [
      SidebarComponent
    ],
    imports     : [
        SharedModule,
        NgbModule,
        RouterModule,
        SidebarCoreModule
    ],
    exports     : [
      SidebarComponent
    ]
})
export class SidebarModule
{
}
