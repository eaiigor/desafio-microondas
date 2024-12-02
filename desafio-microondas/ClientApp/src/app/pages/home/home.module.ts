import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HomeComponent} from "./home.component";
import {RouterModule} from "@angular/router";
import {MicrowaveModule} from "../../components/microwave/microwave.module";


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    MicrowaveModule,
    RouterModule.forChild([{
      path: '',
      component: HomeComponent
    }]),
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule {
}
