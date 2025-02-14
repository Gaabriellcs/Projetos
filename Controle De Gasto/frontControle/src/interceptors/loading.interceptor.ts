import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { LoadingService } from '../services/loading.service';

export const loadingSpinnerInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService); // ✅ Injeta o serviço corretamente
  loadingService.show();

  return next(req).pipe(finalize(() => loadingService.hide())); // ✅ Esconde o loading ao finalizar a requisição
};
