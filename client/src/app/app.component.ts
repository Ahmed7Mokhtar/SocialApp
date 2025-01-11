import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  constructor(private accountService: AccountService) {
    
  }
  
  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString)
      return;
    
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
