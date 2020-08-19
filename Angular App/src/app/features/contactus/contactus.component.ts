import { Component, OnInit } from '@angular/core';
import { Contactus } from '../../models/contactus';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-contactus',
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})

export class ContactusComponent implements OnInit {

  private _contactus: Contactus = new Contactus();
  public get contactus(): Contactus {
    return this._contactus;
  }
  public set contactus(value: Contactus) {
    this._contactus = value;
  }
  constructor() { }

  ngOnInit() {
  }

  public save(form: NgForm){
    this._contactus = new Contactus();
  }
}
