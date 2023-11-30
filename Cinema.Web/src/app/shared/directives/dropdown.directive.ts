import { Directive, HostListener, HostBinding } from '@angular/core';

@Directive({
  selector: '[cinemaDropdown]'
})
export class DropdownDirective {
  @HostBinding('class.open') isOpen = false;

  @HostListener('click') click() {
    this.isOpen = !this.isOpen;
  }

  @HostListener('mouseover') mouseover() {
    this.isOpen = !this.isOpen;

  }
  @HostListener('mouseout') mouseout() {
    this.isOpen = false;
  }

}
