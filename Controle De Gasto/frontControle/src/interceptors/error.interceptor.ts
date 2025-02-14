import { HttpInterceptorFn } from '@angular/common/http';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { MessageService } from 'primeng/api';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const messageService = inject(MessageService);
  const router = inject(Router);

  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {

        alert('Você não está autenticado. Por favor, faça login.');
        
        router.navigate(['/login']);
        
        return throwError(() => new Error('Usuário não autenticado'));
      }
      
      alert(error.error.message);

      return throwError(error);
    })
  );
};
