import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  templateUrl: './Welcome.component.html',
  styleUrls: ['./Welcome.component.css']
})
export class WelcomeComponent {
  constructor(private router: Router) { }

  navigateToLibrary() {
    this.router.navigate(['/library']);
  }

  navigateToSearch() {
    this.router.navigate(['/search']);
  }
}
