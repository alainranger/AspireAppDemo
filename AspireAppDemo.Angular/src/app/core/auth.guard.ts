import { CanActivateFn } from '@angular/router';

export const AuthGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem("access_token");

  return token !== null;
};
