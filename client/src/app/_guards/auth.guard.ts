import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, GuardResult, MaybeAsync, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  return accountService.currentUser$.pipe(
    map(user => {
      if (user) 
        return true;
      
      toastr.error('You shall not pass!');
      return false;
    })
  )
};

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthGuard implements  CanActivate {

//   constructor(private accountService: AccountService, private toastr: ToastrService) {}

//   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
//     return this.accountService.currentUser$.pipe(
//       map(user => {
//         if (user) 
//           return true;
        
//         this.toastr.error('You shall not pass!');
//         return false;
//       })
//     )
//   }
// }
