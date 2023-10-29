import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isCollapsed = true;

  constructor() {
    // Empty constructor because we don't need to do anything here
    // If we don't include this, TypeScript will automatically generate an empty constructor for us
    // This is just to make it clear that we don't need to do anything in the constructor
  }

  ngOnInit() {
    console.log('NavComponent initialized');
  }
}
