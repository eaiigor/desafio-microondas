import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MicrowaveComponent} from './microwave.component';
import {FormsModule} from "@angular/forms";
import {HeatingProgramModule} from "../heating-program/heating-program.module";
import {MicrowaveHandlerModule} from "../../services/microwave-handler/microwave-handler.module";
import {HeatingProgramSModule} from "../../services/heating-program/heating-program-s.module";


@NgModule({
  declarations: [
    MicrowaveComponent
  ],
  exports: [
    MicrowaveComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HeatingProgramModule,
    MicrowaveHandlerModule,
    HeatingProgramSModule
  ]
})
export class MicrowaveModule {
}
