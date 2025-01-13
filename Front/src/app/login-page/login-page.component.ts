import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss', '../styles/spinner.scss'],
})
export class LoginPageComponent implements OnInit {
  public loginForm!: FormGroup;
  public errorMessage: string | null = null;
  public loading: boolean = false;
  public isButtonDisabled: boolean = true;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
    this.loginForm.valueChanges.subscribe(() => {
      this.isButtonDisabled = this.loginForm.invalid;
    });
  }

  public onSubmit() {
    this.errorMessage = null;
    this.loading = true; 

    const { username, password } = this.loginForm.value;

    const loginRequest = {
      username: username,
      password: password,
    };

    this.authenticationService.login(loginRequest).subscribe(
      (response) => {
        this.loading = false;
      },
      (error) => {
        this.loading = false;
        this.errorMessage = 'Login failed. Please check your credentials and try again.';
      }
    );
  }
}