import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastro-usuario',
  imports: [ ReactiveFormsModule, NgIf ],
  templateUrl: './cadastro-usuario.component.html',
  styleUrl: './cadastro-usuario.component.scss'
})
export class CadastroUsuarioComponent {

  criarContaForm = new FormGroup({
    senha: new FormControl<string>('', [Validators.required]),
    nome: new FormControl<string>('', [Validators.required]),
    usuario: new FormControl<string>('', [Validators.required]),
  });
  
  criarConta(){
    
  }
}
