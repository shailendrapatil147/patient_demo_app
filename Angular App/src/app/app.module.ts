import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './features/home/welcome.component';
import { PagenotfoundComponent } from './errorpages/pagenotfound.component';
import { ContactusComponent } from './features/contactus/contactus.component';
import { PatientModule } from './features/patients/patient.module';
import { ContactusModule } from './features/contactus/contactus.module';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    PagenotfoundComponent,
    ContactusComponent
  ],
  imports: [
    BrowserModule,
    PatientModule,
    FormsModule,
    HttpClientModule,
    ContactusModule,
    AppRoutingModule    
  ],
  exports: [],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
