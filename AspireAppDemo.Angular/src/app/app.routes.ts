import { Routes } from '@angular/router';
import { UserProfileComponent } from '../user-profile/user-profile.component';
import { AuthGuard } from './core/auth.guard';
import { LoginComponent } from '../login/login.component';


export const routes: Routes = [
  {
    path: 'user',
    component: UserProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  }
];
