import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public email: string | undefined;
  public password: string | undefined;

  private readonly POST_LOGIN = 'https://localhost:7323/auth/login';

  constructor(
    private http: HttpClient
  ) { }

  onLoginSubmit() {
    this
      .http
      .post<{ accessToken: string }>(
        this.POST_LOGIN,
        {
          Username: this.email,
          Password: this.password
        })
      .subscribe(
        token => {
          localStorage.setItem('access_token', token.accessToken);
        });
  }
}
