import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {


  const strLocal = localStorage.getItem('token');
  if (!strLocal) {
    document.location = '/login'
    return false;
  }
  return true;
};