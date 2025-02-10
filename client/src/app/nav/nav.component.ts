import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent implements OnInit {
  
  model: any = {};
  isLoading = false;
  
  constructor(public accountService: AccountService, private router: Router, private toaster: ToastrService) {}
  
  ngOnInit(): void {
  }

  login() {
    this.isLoading = true;
    this.accountService.login(this.model).subscribe({
      next: () => {},
      error: err => {
        this.isLoading = false;
      },
      complete: () => {
        this.isLoading = false;
        this.router.navigateByUrl('/members');
      }
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
