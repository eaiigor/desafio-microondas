import {NgModule} from "@angular/core";
import {NavMenuComponent} from "./nav-menu.component";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [NavMenuComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavMenuComponent]
})
export class NavMenuModule {
}
