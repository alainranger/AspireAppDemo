import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SubscribeRequest } from '../../models/user.model';

@Component({
  selector: 'app-subscription',
  imports: [FormsModule],
  templateUrl: './subscription.component.html',
  styleUrl: './subscription.component.scss'
})
export class SubscriptionComponent {
  public lastname: string | undefined;
  public firstname: string | undefined;
  public email: string | undefined;
  public password: string | undefined;

  private readonly POST_SUBSCRIBE = 'https://localhost:7323/auth/subscribe';

  constructor(
    private http: HttpClient
  ) { }


  onSubmit() {
    let request = new SubscribeRequest(
      this.lastname,
      this.firstname,
      this.email,
      this.password
    );
    this
      .http
      .post(
        this.POST_SUBSCRIBE,
        request)
      .subscribe();
  }
}
