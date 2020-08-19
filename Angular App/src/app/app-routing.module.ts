import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WelcomeComponent } from './features/home/welcome.component';
import { PagenotfoundComponent } from './errorpages/pagenotfound.component';
import { ContactusComponent } from './features/contactus/contactus.component';

const routes: Routes = [{path: 'welcome', component: WelcomeComponent},
                        {path: 'contactus', component: ContactusComponent},
                        {path: '', component: WelcomeComponent, pathMatch: 'full'},
                        {path: '**', component: PagenotfoundComponent, pathMatch: 'full'}];

@NgModule({
  imports: [ RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
