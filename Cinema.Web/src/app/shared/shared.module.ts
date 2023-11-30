import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropdownDirective } from './directives/dropdown.directive';

@NgModule({
  declarations: [DropdownDirective],
  imports: [CommonModule, NgxDatatableModule, FormsModule, ReactiveFormsModule],
  exports: [
    CommonModule,
    NgxDatatableModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownDirective,
  ],
})
export class SharedModule {}
