import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CoursesModule } from 'app/modules/courses/courses.module';
import { AuthModule } from './authentification/auth.module';

@NgModule({
    imports: [
        AuthModule,
        CoursesModule
    ]
})
export class CoreModule
{
    /**
     * Constructor
     */
    constructor(
        @Optional() @SkipSelf() parentModule?: CoreModule
    )
    {
        // Do not allow multiple injections
        if ( parentModule )
        {
            throw new Error('CoreModule has already been loaded. Import this module in the AppModule only.');
        }
    }
}
