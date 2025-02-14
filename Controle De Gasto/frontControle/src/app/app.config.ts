import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import AuraLight from '@primeng/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './interceptors/auth.interceptor';
import { errorInterceptor } from './interceptors/error.interceptor';
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [MessageService,
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient(withFetch()),
    provideAnimations(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])), // ✅ Registra o interceptor aqui!
    provideHttpClient(withInterceptors([errorInterceptor])), // ✅ Registra o interceptor aqui!
    provideRouter(routes), // ✅ Mantém o roteamento funcionando
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
