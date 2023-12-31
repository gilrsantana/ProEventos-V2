import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  isCollapsed = true;

  constructor(private router: Router) {
    // Empty constructor because we don't need to do anything here
    // If we don't include this, TypeScript will automatically generate an empty constructor for us
    // This is just to make it clear that we don't need to do anything in the constructor
  }

  ngOnInit(): void {
    console.log('NavComponent initialized');
  }

  showMenu(): boolean {
    return this.router.url !== '/user/login';
  } 
}
