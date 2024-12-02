import {Directive, HostListener} from "@angular/core";

@Directive({
  standalone: true,
  selector: "[stop-propagation-directive]"
})
export class StopPropagationDirective
{
    @HostListener("click", ["$event"])
    public onClick(event: any): void
    {
        event.stopPropagation();
    }
}
