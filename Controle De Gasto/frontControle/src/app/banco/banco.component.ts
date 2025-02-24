import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule, NgIf } from '@angular/common';
import { BancoService } from '../../services/banco.service';


@Component({
  selector: 'app-banco',
  imports: [FormsModule, InputTextModule, ButtonModule, TableModule, CommonModule, NgIf],
  templateUrl: './banco.component.html',
  styleUrl: './banco.component.scss'
})
export class BancoComponent {
  cadastrobanco: string = "";
  codigoBanco: any;
  bancos!: any[];

  constructor(private srv: BancoService) { }

  ngOnInit(): void {
    this.listaBanco();
  }

  listaBanco(){
    this.srv.ListaBanco(false).subscribe({
      next: p=>{
        this.bancos = p;
      }
    })

  }

  cadastraBanco() {
    this.srv.Cadastrar(this.cadastrobanco, this.codigoBanco).subscribe({
      next: p => {
        this.listaBanco();
      }
    })
  }

  inativar(id: number){
    this.srv.Inativar(id).subscribe({
      next: p =>{
        this.listaBanco();
      }
    })
  }

  ativar(id: number){
    this.srv.Ativar(id).subscribe({
      next: p =>{
        this.listaBanco();
      }
    })
  }
}
