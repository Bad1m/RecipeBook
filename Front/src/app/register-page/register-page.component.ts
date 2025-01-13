import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { RegisterRequest } from '../models/auth-models';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss', '../styles/spinner.scss'],
})
export class RegisterPageComponent implements OnInit {
  public registerForm!: FormGroup;
  public errorMessage: string | null = null;
  public loading: boolean = false; 
  public isButtonDisabled: boolean = true;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit() {
    this.registerForm = new FormGroup({
      firstname: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
    });

    this.registerForm.valueChanges.subscribe(() => {
      this.isButtonDisabled = this.registerForm.invalid;
    });
  }

  public onSubmit() {
    this.errorMessage = null;
    this.loading = true;

    const registerRequest: RegisterRequest = {
      firstname: this.registerForm.get('firstname')!.value,
      lastname: this.registerForm.get('lastname')!.value,
      username: this.registerForm.get('username')!.value,
      email: this.registerForm.get('email')!.value,
      password: this.registerForm.get('password')!.value,
    };

    this.authenticationService.register(registerRequest).subscribe(
      (token) => {
        this.loading = false;
      },
      (error) => {
        this.loading = false;
        this.errorMessage = 'Registration failed. Please check your details and try again.';
      }
    );
  }
}