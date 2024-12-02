import {NgModule} from '@angular/core';
import {HeatingProgramService} from "./heating-program.service";
import {HttpClientModule} from "@angular/common/http";


@NgModule({
  declarations: [],
  imports: [
    HttpClientModule
  ],
  providers: [
    HeatingProgramService
  ],
})
export class HeatingProgramSModule {
}
