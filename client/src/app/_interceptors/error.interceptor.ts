import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);
  return next(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        if(error) {
          switch (error.status) {
            case 400:
              if(error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if(error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }

                throw modalStateErrors.flat();
              } else {
                toastr.error(error.error, error.status.toString());
              }
              break;
            case 401:
              toastr.error('Unauthorized', error.status.toString())
              break;
            case 404:
              router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = {
                state: {
                  error: error.error
                }
              }

              router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }

        throw error;
      })
    );
};


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private toastr: ToastrService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if(error) {
            switch (error.status) {
              case 400:
                if(error.error.errors) {
                  const modalStateErrors = [];
                  for (const key in error.error.errors) {
                    if(error.error.errors[key]) {
                      modalStateErrors.push(error.error.errors[key]);
                    }
                  }

                  throw modalStateErrors;
                } else {
                  this.toastr.error(error.error, error.status.toString());
                }
                break;
              case 401:
                this.toastr.error('Unauthorized', error.status.toString())
                break;
              case 404:
                this.router.navigateByUrl('/not-found');
                break;
              case 500:
                const navigationExtras: NavigationExtras = {
                  state: {
                    error: error.error
                  }
                }

                this.router.navigateByUrl('/server-error', navigationExtras);
                break;
              default:
                this.toastr.error('Something unexpected went wrong');
                console.log(error);
                break;
            }
          }

          throw error;
        })
      )
  }

}
