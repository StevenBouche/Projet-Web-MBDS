import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'app/shared/shared.module';
import { NavigationComponent } from './navigation.component';

@NgModule({
    declarations: [
      NavigationComponent
    ],
    imports     : [
        SharedModule,
        NgbModule
    ],
    exports     : [
      NavigationComponent
    ]
})
export class NavigationModule
{
}
