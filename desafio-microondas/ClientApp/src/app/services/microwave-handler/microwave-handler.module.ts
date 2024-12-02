import {NgModule} from '@angular/core';
import {MicrowaveHandlerService} from "./microwave-handler.service";
import {HttpClientModule} from "@angular/common/http";


@NgModule({
  declarations: [],
  imports: [
    HttpClientModule
  ],
  providers: [
    MicrowaveHandlerService
  ],
})
export class MicrowaveHandlerModule {
}
