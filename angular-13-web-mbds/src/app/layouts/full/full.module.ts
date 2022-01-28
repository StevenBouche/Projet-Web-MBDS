import { NgModule } from '@angular/core';
import { NavigationModule } from 'app/shared/header/navigation.module';
import { SharedModule } from 'app/shared/shared.module';
import { FullComponent } from './full.component';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from 'app/shared/sidebar/sidebar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SidebarModule } from 'app/shared/sidebar/sidebar.module';


@NgModule({
    declarations: [
      FullComponent
    ],
    imports     : [
        SharedModule,
        RouterModule,
        NgbModule,
        NavigationModule,
        SidebarModule
    ],
    exports     : [
      FullComponent
    ]
})
export class FullModule
{
}
