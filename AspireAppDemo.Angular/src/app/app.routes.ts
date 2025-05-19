import { Routes } from '@angular/router';
import { UserProfileComponent } from './user/profile/profile.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginComponent } from './auth/login/login.component';
import { SubscriptionComponent } from './auth/subscription/subscription.component';


export const routes: Routes = [
  {
    path: 'user',
    component: UserProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'subscribe',
    component: SubscriptionComponent
  }
];
