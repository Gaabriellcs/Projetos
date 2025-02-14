import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cadastro-usuario',
  imports: [ ReactiveFormsModule, NgIf, DialogModule ],
  templateUrl: './cadastro-usuario.component.html',
  styleUrl: './cadastro-usuario.component.scss'
})
export class CadastroUsuarioComponent {

  constructor (private srv: UsuarioService,  private router: Router){}

  criarContaForm = new FormGroup({
    Senha: new FormControl<string>('', [Validators.required]),
    Nome: new FormControl<string>('', [Validators.required]),
    Usuario: new FormControl<string>('', [Validators.required]),
  });
  
  criarConta(){
    this.srv.CriaConta(this.criarContaForm.value).subscribe({
      next: p => {
        this.showSuccessModal();
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      }
    })
  }

  displaySuccess: boolean = false;

  showSuccessModal() {
    this.displaySuccess = true;
  }

  hideSuccessModal() {
    this.displaySuccess = false;
  }

}
