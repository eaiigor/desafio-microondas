import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HeatingProgramComponent} from "./heating-program.component";
import {HeatingProgramSModule} from "../../services/heating-program/heating-program-s.module";
import {MatDialogModule} from "@angular/material/dialog";
import {FormsModule} from "@angular/forms";
import {AddItemDialogComponent} from "../add-item-dialog/add-item-dialog.component";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {StopPropagationDirective} from "../../directives/stop-propagation.directive";


@NgModule({
  declarations: [
    HeatingProgramComponent,
    AddItemDialogComponent
  ],
  imports: [
    CommonModule,
    HeatingProgramSModule,
    MatDialogModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    StopPropagationDirective,
  ],
  exports: [
    HeatingProgramComponent
  ]
})
export class HeatingProgramModule {
}
