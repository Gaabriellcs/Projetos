import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import AuraLight from '@primeng/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { authInterceptor } from '../interceptors/auth.interceptor';
import { errorInterceptor } from '../interceptors/error.interceptor';
import { MessageService } from 'primeng/api';
import { loadingSpinnerInterceptor } from '../interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    MessageService,
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideAnimations(),
    provideRouter(routes),

    // ✅ Agora todos os interceptors são registrados de uma vez
    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor, errorInterceptor, loadingSpinnerInterceptor])
    ),

    providePrimeNG({
      theme: {
        preset: AuraLight,
        options: {
          darkModeSelector: false || 'none',
        },
      },
    }),
  ],
};
