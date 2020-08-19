import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ContactusComponent } from './contactus.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild([{path:'contactus', component: ContactusComponent}])
  ]
})
export class ContactusRoutingModule { }
