import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {

  baseUrl = 'https://localhost:5001/api/'
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {
    
  }

  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found')
      .subscribe({
        next: response => console.log(response),
        error: err => console.log(err)
      })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request')
      .subscribe({
        next: response => console.log(response),
        error: err => console.log(err)
      })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error')
      .subscribe({
        next: response => console.log(response),
        error: err => console.log(err)
      })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth')
      .subscribe({
        next: response => console.log(response),
        error: err => console.log(err)
      })
  }
  
  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register', {})
      .subscribe({
        next: response => console.log(response),
        error: err => {
          console.log(err);
          this.validationErrors = err;
        }
      })
  }
}
