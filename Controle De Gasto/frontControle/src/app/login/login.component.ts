import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, DialogModule, ReactiveFormsModule, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {


  loginForm = new FormGroup({
    Usuario: new FormControl<string>('', [Validators.required]),
    Senha: new FormControl<string>('', [Validators.required])
  })

  constructor(private srv: UsuarioService, private router: Router) { }

  login() {
    this.srv.login(this.loginForm.value).subscribe({
      next: p => { 
        this.router.navigate(['/']);
        localStorage.setItem('token', p.token);
      }
    })
  }

}
