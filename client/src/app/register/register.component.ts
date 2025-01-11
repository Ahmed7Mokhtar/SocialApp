import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService, private toaster: ToastrService) { }

  register() {
    this.accountService.register(this.model).subscribe({
      error: err => {
        this.toaster.error(err.error);
      },
      complete: () => {
        this.cancel();
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
